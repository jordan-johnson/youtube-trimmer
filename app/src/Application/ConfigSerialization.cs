using System;
using System.IO;
using Newtonsoft.Json;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public class ConfigSerialization
    {
        private const string _configFile = "config.json";
        public ConfigModel Model { get; private set; }

        public ConfigSerialization()
        {
            if(File.Exists(_configFile))
            {
                ReadConfig();
            }
            else
            {
                WriteConfig();
                ReadConfig();
            }
        }

        private void ReadConfig()
        {
            if(File.Exists(_configFile))
            {
                using (StreamReader streamReader = File.OpenText(_configFile))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    Model = (ConfigModel)serializer.Deserialize(streamReader, typeof(ConfigModel));
                }
            }
        }

        private void WriteConfig()
        {
            using (FileStream filestream = File.Open(_configFile, FileMode.OpenOrCreate))
            using (StreamWriter streamwriter = new StreamWriter(filestream))
            using (JsonTextWriter jsonwriter = new JsonTextWriter(streamwriter))
            {
                try
                {
                    jsonwriter.Formatting = Formatting.Indented;

                    ConfigModel defaultConfig = new ConfigModel
                    {
                        FFMpegDirectory = "/usr/local/bin/ffmpeg",
                        DownloadDirectory = "downloads",
                        OutputFileNameTemplate = "{0}_trimmed.{1}"
                    };

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jsonwriter, defaultConfig);
                }
                catch
                {
                    throw new ConfigSerializationException(_configFile, "Serializer failed to write configuration file.");
                }
            }
        }
    }
}