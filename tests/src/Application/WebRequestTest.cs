using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class WebRequestTest : IDisposable
    {
        private ConfigModel _config;

        private const string _url = "https://old.reddit.com/r/Overwatch/comments/9x5h5q/booobbb_do_somthi_oh/.json";
        private const string _filename = "test.json";
        private string _path;

        public WebRequestTest()
        {
            _config = new ConfigSerialization().Model;
            _path = FormatFilePath(_filename);

            WebRequest.OnProgressChange += TestDownloadProgressChange;
            WebRequest.OnCompletion += TestDownloadProgressFinish;
        }

        public void Dispose()
        {
            WebRequest.OnProgressChange -= TestDownloadProgressChange;
            WebRequest.OnCompletion -= TestDownloadProgressFinish;

            if(File.Exists(_path))
                File.Delete(_path);
        }

        [Fact]
        public async void TestDownload()
        {
            Task download = DownloadTestFile();
            await download;

            bool fileExists = File.Exists(_path);
            Assert.True(fileExists);
        }

        private string FormatFilePath(string filename)
        {
            return string.Format("{0}/{1}", _config.DownloadDirectory, filename);
        }

        private async Task DownloadTestFile()
        {
            bool isFileDownloaded = File.Exists(_path);
            if(!isFileDownloaded)
                await WebRequest.Download(_url, _config.DownloadDirectory, _filename);
        }

        private void TestDownloadProgressChange(float percentage)
        {
            Console.Write("\rDownloading...{0}%", percentage);
        }

        private void TestDownloadProgressFinish()
        {
            Console.WriteLine("\nDownload complete.");
        }
    }
}