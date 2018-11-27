using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class WebRequestTest : IDisposable
    {
        public WebRequestTest()
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
        public void TestDownload()
        {
            string url = "https://old.reddit.com/r/Overwatch/comments/9x5h5q/booobbb_do_somthi_oh/.json";
            string dir = "downloads";
            string filename = "test.json";
            string path = dir + "/" + filename;

            WebRequest.Download(url, dir, filename);

            bool fileExists = File.Exists(path);
            Assert.True(fileExists);

            if(fileExists)
                File.Delete(path);
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