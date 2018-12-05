using System;
using System.IO;
using System.Linq;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace YTTrimmer.Application
{
    public class Trimmer
    {
        private ConfigModel _config;

        public Trimmer(ConfigModel config)
        {
            _config = config;
        }

        public string Run(string filename, double start, double span)
        {
            var path = _config.DownloadDirectory + filename;
            var input = new MediaFile {Filename = path};
            var output = GenerateOutputMediaFile(filename);

            using(Engine engine = new Engine(_config.FFMpegDirectory))
            {
                engine.GetMetadata(input);

                ConversionOptions options = new ConversionOptions();
                options.CutMedia(TimeSpan.FromSeconds(start), TimeSpan.FromSeconds(span));

                engine.Convert(input, output, options);
            }

            return output.Filename;
        }

        private MediaFile GenerateOutputMediaFile(string filename)
        {
            var split = filename.Split(".");
            var outputName = string.Format(_config.OutputFileNameTemplate, split[0], split[1]);
            var path = _config.DownloadDirectory + outputName;

            return new MediaFile {Filename = path};
        }
    }
}