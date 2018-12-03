using System;
using System.Net;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace YTTrimmer.Application
{
    public class WebRequest
    {
        private ConfigModel _config;

        public WebRequest(ConfigModel config)
        {
            _config = config;

            /**
             * Fixes WebException thrown "Decoded string is not a valid IDN name"
             * when attempting to download Youtube video files. 
             */
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
        }

        public async Task<string> Download(string url, string filename)
        {
            string path = _config.DownloadDirectory + filename;

            using(var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.Unicode;

                if(!Directory.Exists(_config.DownloadDirectory))
                    Directory.CreateDirectory(_config.DownloadDirectory);

                //await client.DownloadFileTaskAsync(url, path);
                await client.DownloadFileTaskAsync(new System.Uri(url), path);
            }

            return path;
        }
    }
}