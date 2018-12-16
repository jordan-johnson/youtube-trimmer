using System;
using System.IO;
using Newtonsoft.Json;
using YTTrimmer.Application.Models;
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

            CreateDownloadDirectoryIfNotExists();
        }

        private void ReadConfig()
        {
            try
            {
                using (StreamReader streamReader = File.OpenText(_configFile))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    Model = (ConfigModel)serializer.Deserialize(streamReader, typeof(ConfigModel));
                }
            }
            catch
            {
                throw new ConfigSerializationException(_configFile, "Could not read configuration file.");
            }
        }

        private void WriteConfig()
        {
            try
            {
                using (FileStream filestream = File.Open(_configFile, FileMode.OpenOrCreate))
                using (StreamWriter streamwriter = new StreamWriter(filestream))
                using (JsonTextWriter jsonwriter = new JsonTextWriter(streamwriter))
                {
                    jsonwriter.Formatting = Formatting.Indented;

                    var defaultConfig = new ConfigModel
                    {
                        FFMpegDirectory = "/usr/local/bin/ffmpeg",
                        DownloadDirectory = "downloads",
                        OutputFileNameTemplate = "{0}_trimmed.{1}"
                    };

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jsonwriter, defaultConfig);
                }
            }
            catch
            {
                throw new ConfigSerializationException(_configFile, "Serializer failed to write configuration file.");
            }
        }

        private void CreateDownloadDirectoryIfNotExists()
        {
            if(!Directory.Exists(Model.DownloadDirectory))
            {
                Directory.CreateDirectory(Model.DownloadDirectory);
            }
        }
    }
}