using System;
using System.IO;
using System.Linq;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public class Trimmer
    {
        private ConfigModel _config;
        private MediaFile _inputMediaFile;
        private MediaFile _outputMediaFile;
        private TimeDataModel _timeData;

        public Trimmer(ConfigModel config)
        {
            _config = config;
        }

        public void SetInputFile(string filename)
        {
            _inputMediaFile = new MediaFile {Filename = _config.DownloadDirectory + filename};
            
            GenerateOutputFile(filename);
        }

        public void SetTimeData(string start, string end)
        {
            _timeData = new TimeDataModel(start, end);
        }

        public string Run()
        {
            try
            {
                using(Engine engine = new Engine(_config.FFMpegDirectory))
                {
                    engine.GetMetadata(_inputMediaFile);

                    ConversionOptions options = new ConversionOptions();
                    options.CutMedia(_timeData.Start, _timeData.GetDifference);

                    engine.Convert(_inputMediaFile, _outputMediaFile, options);
                }

                return _outputMediaFile.Filename;
            }
            catch
            {
                throw new TrimmerException(_inputMediaFile, _timeData, "Could not trim video.");
            }
        }

        private void GenerateOutputFile(string filename)
        {
            var split = filename.Split(".");
            var outputName = string.Format(_config.OutputFileNameTemplate, split[0], split[1]);
            var outputPath = _config.DownloadDirectory + outputName;

            _outputMediaFile = new MediaFile {Filename = outputPath};
        }
    }
}