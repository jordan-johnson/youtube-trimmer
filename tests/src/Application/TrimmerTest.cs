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
        private WebRequest _web;
        private Trimmer _trimmer;
        
        private const string _url       = "https://v.redd.it/i2h1g7n9sdy11/DASH_9_6_M";
        private const string _dir       = "downloads";
        private const string _filename  = "trimtest.mp4";
        private const string _path      = _dir + "/" + _filename;
        private const string _trimPath  = _dir + "/trimtest_trimmed.mp4";

        public TrimmerTest()
        {
            _config = new ConfigSerialization().Model;
            _web = new WebRequest(_config);
            _trimmer = new Trimmer(_config);
        }

        public void Dispose()
        {
            if(File.Exists(_path))
                File.Delete(_path);

            if(File.Exists(_trimPath))
                File.Delete(_trimPath);
        }

        [Fact]
        public async void TestTrim()
        {
            bool isFileDownloaded = File.Exists(_path);
            if(!isFileDownloaded)
            {
                Task download = DownloadTestFile();
                await download;
            }

            _trimmer.Run(_filename, 10, 10);

            bool trimFileExists = File.Exists(_trimPath);
            Assert.True(trimFileExists);
        }

        private async Task DownloadTestFile()
        {
            bool isFileDownloaded = File.Exists(_path);
            if(!isFileDownloaded)
                await _web.Download(_url, _filename);
        }
    }
}