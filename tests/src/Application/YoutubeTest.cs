using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.Tests.Application
{
    public class YoutubeTest : IDisposable
    {
        private ConfigModel _config;
        private YoutubeHandler _ytHandler;

        private const string _ytAddress = "https://www.youtube.com/watch?v=C0DPdy98e4c";

        public YoutubeTest()
        {
            _config = new ConfigSerialization().Model;
            _ytHandler = new YoutubeHandler(_config);
        }

        public void Dispose()
        {
            if(File.Exists("config.json"))
                File.Delete("config.json");
                
            foreach(var model in _ytHandler.GetDownloadedFromQueue())
            {
                if(model.FileExists)
                {
                    File.Delete(model.Path);
                }
            }
        }

        [Fact]
        public void TestDownload()
        {
            _ytHandler.ClearQueue();
            _ytHandler.Queue(_ytAddress);
            _ytHandler.DownloadQueue().Wait();

            var downloads = _ytHandler.GetDownloadedFromQueue();
            var videoId = _ytHandler.ParseVideoId(_ytAddress);
            var model = _ytHandler.GetModelByVideoId(videoId);

            Assert.True(model.FileExists);
        }

        [Fact]
        public void TestDownloadMultiple()
        {
            List<string> urls = new List<string>
            {
                "https://www.youtube.com/watch?v=b2jDTmGVyxs",
                "https://www.youtube.com/watch?v=YNAiAH189n0"
            };

            _ytHandler.ClearQueue();
            _ytHandler.Queue(urls);
            _ytHandler.DownloadQueue().Wait();

            foreach(var model in _ytHandler.GetDownloadedFromQueue())
            {
                Assert.True(model.FileExists);
            }
        }
    }
}