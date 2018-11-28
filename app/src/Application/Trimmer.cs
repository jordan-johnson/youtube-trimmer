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

        public void Run(string filename, double start, double span)
        {
            var path = FormatFilePath(filename);
            var input = new MediaFile {Filename = path};
            var output = GenerateOutputMediaFile(filename);

            using(Engine engine = new Engine(_config.FFMpegDirectory))
            {
                engine.GetMetadata(input);

                ConversionOptions options = new ConversionOptions();
                options.CutMedia(TimeSpan.FromSeconds(start), TimeSpan.FromSeconds(span));

                engine.Convert(input, output, options);
            }
        }

        private string FormatFilePath(string filename)
        {
            return string.Format("{0}/{1}", _config.DownloadDirectory, filename);
        }

        private MediaFile GenerateOutputMediaFile(string filename)
        {
            var split = filename.Split(".");
            var outputName = string.Format(_config.OutputFileNameTemplate, split[0], split[1]);
            var path = FormatFilePath(outputName);

            return new MediaFile {Filename = path};
        }
    }
}