using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.Tests.Application
{
    public class ConfigTest : IDisposable
    {
        private ConfigModel _config;

        public ConfigTest()
        {
            _config = new ConfigSerialization().Model;
        }

        public void Dispose()
        {
            if(File.Exists("config.json"))
                File.Delete("config.json");
        }

        [Fact]
        public void TestConfigGetData()
        {
            bool configExists = File.Exists("config.json");
            Assert.True(configExists);

            Assert.NotNull(_config.FFMpegDirectory);
            Assert.NotNull(_config.DownloadDirectory);
            Assert.NotNull(_config.OutputFileNameTemplate);
        }
    }
}