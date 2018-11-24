using System;
using System.IO;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class WebRequestTest
    {
        [Fact]
        public void TestDownload()
        {
            string url = "https://old.reddit.com/r/Overwatch/comments/9x5h5q/booobbb_do_somthi_oh/.json";
            string path = "test.json";

            WebRequest.Download(url, "downloads", path);

            bool fileExists = File.Exists(path);
            Assert.True(fileExists);

            if(fileExists)
                File.Delete(path);
        }
    }
}