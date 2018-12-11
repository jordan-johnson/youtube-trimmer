using System;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public partial class YoutubeHandler
    {
        public string ParseVideoId(string url)
        {
            string videoId;

            if(!YoutubeClient.TryParseVideoId(url, out videoId))
                throw new YoutubeHandlerException(url, "Video Id was not found in URL.");
            
            return videoId;
        }

        public YoutubeModel GetModelByVideoId(string videoId)
        {
            try
            {
                return _queue.Find(x => x.Id == videoId);
            }
            catch
            {
                throw new YoutubeHandlerException(videoId, "Model not found in queue.");
            }
        }

        public YoutubeModel GetModelByVideoAddress(string url)
        {
            var videoId = ParseVideoId(url);

            return GetModelByVideoId(videoId);
        }

        private void CreateDownloadDirectoryIfNotExists()
        {
            if(!Directory.Exists(_config.DownloadDirectory))
            {
                Directory.CreateDirectory(_config.DownloadDirectory);
            }
        }
    }
}