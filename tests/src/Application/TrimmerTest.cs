using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.Tests.Application
{
    public class TrimmerTest : IDisposable
    {
        private ConfigModel _config;
        private YoutubeHandler _ytHandler;
        private Trimmer _trimmer;
        private IVideoModel _model;
        
        private const string _url = "https://www.youtube.com/watch?v=C0DPdy98e4c";
        private const string _trimmedPath = "downloads/C0DPdy98e4c_trimmed.mp4";

        public TrimmerTest()
        {
            _config = new ConfigSerialization().Model;
            _ytHandler = new YoutubeHandler(_config);
            _trimmer = new Trimmer(_config);
        }

        public void Dispose()
        {
            if(File.Exists("config.json"))
                File.Delete("config.json");

            if(_model.FileExists)
                File.Delete(_model.Path);

            if(File.Exists(_trimmedPath))
                File.Delete(_trimmedPath);
        }

        [Fact]
        public void TestTrimVideo()
        {
            _model = DownloadVideoIfNotExists();

            _trimmer.SetTrimSection("00:00:10", "00:00:15");
            _trimmer.Run();
        }

        private IVideoModel DownloadVideoIfNotExists()
        {
            var expectedFilePath = _config.DownloadDirectory + "C0DPdy98e4c.mp4";
            var fileExists = File.Exists(expectedFilePath);

            if(!fileExists)
            {
                _ytHandler.ClearQueue();
                _ytHandler.Queue(_url);
                _ytHandler.DownloadQueue().Wait();
            }

            return _trimmer.Load(expectedFilePath);
        }
    }
}