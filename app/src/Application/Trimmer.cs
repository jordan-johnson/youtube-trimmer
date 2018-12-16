using System;
using System.IO;
using System.Linq;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using YTTrimmer.Application.Models;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public class Trimmer
    {
        private ConfigModel _config;
        private TrimVideoModel _model;
        private Engine _engine;

        public Trimmer(ConfigModel config)
        {
            _config = config;
            _engine = new Engine(_config.FFMpegDirectory);
        }

        public TrimVideoModel Load(string path)
        {
            _model = new TrimVideoModel(path);
            
            _engine.GetMetadata(_model.Input);

            return _model;
        }

        public void SetTrimSection(dynamic start, dynamic end)
        {
            _model.SetTrimTime(start, end);
        }

        public void Run()
        {
            if(_model == null)
                throw new TrimmerException("Video wasn't loaded before operation.");

            try
            {
                _model.CreateOutput(_config.DownloadDirectory, _config.OutputFileNameTemplate);

                ConversionOptions options = new ConversionOptions();
                options.CutMedia(_model.TrimTime.Start, _model.TrimTime.GetDifference());

                _engine.Convert(_model.Input, _model.Output, options);
            }
            catch
            {
                throw new TrimmerException(_model, "Could not trim video.");
            }
        }
    }
}