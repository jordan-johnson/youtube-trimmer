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

        private string _originalPath;
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
            if(File.Exists(_originalPath))
                File.Delete(_originalPath);

            if(File.Exists(_trimmedPath))
                File.Delete(_trimmedPath);
        }

        [Fact]
        public async Task TestDownloadAndTrim()
        {
            var model = await _ytHandler.DownloadVideoAsync(_url);
            var filename = String.Format("{0}.{1}", model.Id, model.Extension);

            _originalPath = model.Path;
            bool originalFileExists = File.Exists(_originalPath);
            Assert.True(originalFileExists);

            _trimmedPath = _trimmer.Run(filename, 10, 10);
            bool trimmedFileExists = File.Exists(_trimmedPath);
            Assert.True(trimmedFileExists);
        }
    }
}