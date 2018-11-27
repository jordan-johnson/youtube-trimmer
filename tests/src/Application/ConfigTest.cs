using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YTTrimmer.Application;

namespace YTTrimmer.Tests.Application
{
    public class ConfigTest : IDisposable
    {
        private ConfigSerialization _serializer;

        public ConfigTest()
        {
            _serializer = new ConfigSerialization();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void TestConfigGetData()
        {
            bool configExists = File.Exists("config.json");
            Assert.True(configExists);

            Console.WriteLine(_serializer.Model.FFMpegDirectory);
        }
    }
}