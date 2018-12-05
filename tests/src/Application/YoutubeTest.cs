using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class YoutubeTest : IDisposable
    {
        private ConfigModel _config;
        private YoutubeHandler _ytHandler;
        private YoutubeModel _ytModel;

        private const string _ytAddress = "https://www.youtube.com/watch?v=C0DPdy98e4c";

        public YoutubeTest()
        {
            _config = new ConfigSerialization().Model;
            _ytHandler = new YoutubeHandler(_config);
        }

        public void Dispose()
        {
            if(File.Exists(_ytModel.Path))
                File.Delete(_ytModel.Path);
        }

        [Fact]
        public async Task TestDownload()
        {
            _ytModel = await _ytHandler.DownloadVideoAsync(_ytAddress);

            Assert.NotNull(_ytModel);

            var fileExists = File.Exists(_ytModel.Path);

            Assert.True(fileExists);
        }

    }
}