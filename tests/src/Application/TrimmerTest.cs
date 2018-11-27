using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class TrimmerTest : IDisposable
    {
        private const string _url       = "https://v.redd.it/i2h1g7n9sdy11/DASH_9_6_M";
        private const string _dir       = "downloads";
        private const string _filename  = "trimtest.mp4";
        private const string _path      = _dir + "/" + _filename;
        private const string _trimPath  = _dir + "/trimtest_trimmed.mp4";

        public TrimmerTest()
        {
            WebRequest.OnProgressChange += TestDownloadProgressChange;
            WebRequest.OnCompletion += TestDownloadProgressFinish;
        }

        public void Dispose()
        {
            WebRequest.OnProgressChange -= TestDownloadProgressChange;
            WebRequest.OnCompletion -= TestDownloadProgressFinish;
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

            Trimmer.Run(_dir + "/", _filename, 10, 10);

            bool trimFileExists = File.Exists(_trimPath);
            Assert.True(trimFileExists);
        }

        private async Task DownloadTestFile()
        {
            bool isFileDownloaded = File.Exists(_path);
            if(!isFileDownloaded)
                await WebRequest.Download(_url, _dir, _filename);
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