using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace YTTrimmer.Application
{
    public class WebRequest
    {
        public delegate void ProgressHandler(float percentage);
        public static event ProgressHandler OnProgressChange;

        public delegate void CompletionHandler();
        public static event CompletionHandler OnCompletion;

        public static async Task Download(string url, string dir, string filename)
        {
            string path = dir + "/" + filename;

            using(WebClient client = new WebClient())
            {
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                client.DownloadProgressChanged += ProgressChanged;
                client.DownloadFileCompleted += DownloadFinished;
                await client.DownloadFileTaskAsync(new System.Uri(url), path);
            }
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if(OnProgressChange != null)
            {
                OnProgressChange(e.ProgressPercentage);
            }
        }

        private static void DownloadFinished(object sender, AsyncCompletedEventArgs e)
        {
            if(OnCompletion != null)
            {
                OnCompletion();
            }
        }
    }
}