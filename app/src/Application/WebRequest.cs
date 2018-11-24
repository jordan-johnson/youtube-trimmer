using System;
using System.Net;
using System.IO;

namespace YTTrimmer.Application
{
    public class WebRequest
    {
        private static float _progress = 0;

        public static float GetProgress
        {
            get
            {
                return _progress;
            }
        }

        public static void Download(string url, string dir, string filename)
        {
            _progress = 0;

            string path = dir + "/" + filename;

            using(WebClient client = new WebClient())
            {
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                client.DownloadProgressChanged += ProgressChanged;
                client.DownloadFileAsync(new System.Uri(url), path);
            }
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rDownloading...{0}%", e.ProgressPercentage);

            _progress = e.ProgressPercentage;
        }
    }
}