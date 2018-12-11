using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class TrimmerTest : IDisposable
    {
        private ConfigModel _config;
        private YoutubeHandler _ytHandler;
        private Trimmer _trimmer;
        private YoutubeModel _model;
        private string _trimmedPath;
        
        private const string _url = "https://www.youtube.com/watch?v=C0DPdy98e4c";

        public TrimmerTest()
        {
            _config = new ConfigSerialization().Model;
            _ytHandler = new YoutubeHandler(_config);
            _trimmer = new Trimmer(_config);
        }

        public void Dispose()
        {
            if(_model.FileExists)
                File.Delete(_model.Path);

            if(File.Exists(_trimmedPath))
                File.Delete(_trimmedPath);
        }

        [Fact]
        public void TestTrimVideo()
        {
            DownloadVideoIfNotExists();

            Assert.True(_model.FileExists);

            _trimmer.SetInputFile(_model.FileName);
            _trimmer.SetTimeData("00:00:10", "00:00:15");
            _trimmedPath = _trimmer.Run();

            var trimVideoExists = File.Exists(_trimmedPath);
            Assert.True(trimVideoExists);
        }

        private void DownloadVideoIfNotExists()
        {
            var expectedFilePath = _config.DownloadDirectory + "C0DPdy98e4c.mp4";
            var fileExists = File.Exists(expectedFilePath);

            if(!fileExists)
            {
                _ytHandler.ClearQueue();
                _ytHandler.QueueVideo(_url);
                _ytHandler.DownloadQueue().Wait();

                _model = _ytHandler.GetModelByVideoAddress(_url);
            }
        }
    }
}