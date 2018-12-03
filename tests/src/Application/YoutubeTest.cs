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
        private WebRequest _web;
        private YoutubeHandler _ytHandler;
        private YoutubeModel _ytModel;

        private const string _ytAddress = "https://www.youtube.com/watch?v=C0DPdy98e4c";
        private const string _ytVideoId = "C0DPdy98e4c";

        public YoutubeTest()
        {
            _config = new ConfigSerialization().Model;
            _web = new WebRequest(_config);
            _ytHandler = new YoutubeHandler(_web);
        }

        public void Dispose()
        {
            if(File.Exists(_ytModel.Path))
                File.Delete(_ytModel.Path);
        }

        [Fact]
        public async void TestDownloadWithFullAddress()
        {
            _ytModel = await _ytHandler.DownloadBestResolution(_ytAddress);

            VerifyYoutubeModel();
        }

        [Fact]
        public async void TestDownloadWithIdOnly()
        {
            _ytModel = await _ytHandler.DownloadBestResolution(_ytVideoId);

            VerifyYoutubeModel();
        }

        private void VerifyYoutubeModel()
        {
            Assert.NotNull(_ytModel);
            Assert.NotEmpty(_ytModel.Id);
            Assert.NotEmpty(_ytModel.Path);
            Assert.NotNull(_ytModel.Info);

            var doesFileExist = File.Exists(_ytModel.Path);
            Assert.True(doesFileExist);
        }
    }
}