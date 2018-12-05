using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public class YoutubeHandler
    {
        private ConfigModel _config;
        private YoutubeClient _client;

        public YoutubeHandler(ConfigModel config)
        {
            /**
             * Fixes WebException thrown "Decoded string is not a valid IDN name"
             * when attempting to download Youtube video files. 
             */
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            
            _config = config;
            _client = new YoutubeClient();
        }

        public string ParseVideoId(string url)
        {
            string videoId;

            if(!YoutubeClient.TryParseVideoId(url, out videoId))
                throw new YoutubeHandlerException(url, "Video Id was not found in URL.");
            
            return videoId;
        }

        public async Task<YoutubeModel> DownloadVideoAsync(string url)
        {
            var metadata = await GetVideoMetadataAsync(url);
            var streamInfo = await GetStreamInfoAsync(url);
            var path = await DownloadVideoStreamAsync(url, streamInfo);

            return CreateModel(metadata, streamInfo, path);
        }

        private async Task<Video> GetVideoMetadataAsync(string url)
        {
            try
            {
                string videoId = ParseVideoId(url);

                return await _client.GetVideoAsync(videoId);
            }
            catch
            {
                throw new YoutubeHandlerException(url, "Could not pull metadata.");
            }
        }

        private async Task<MediaStreamInfo> GetStreamInfoAsync(string url)
        {
            try
            {
                var videoId = ParseVideoId(url);
                var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(videoId);

                return streamInfoSet.Muxed.WithHighestVideoQuality();
            }
            catch(System.Exception e)
            {
                throw new YoutubeHandlerException(url, "Could not get media stream info set." + e.Message);
            }
        }

        private async Task<string> DownloadVideoStreamAsync(string url, MediaStreamInfo streamInfo)
        {
            string videoId = ParseVideoId(url);

            try
            {
                var extension = streamInfo.Container.GetFileExtension();
                var filename = String.Format("{0}.{1}", videoId, extension);
                var path = _config.DownloadDirectory + filename;

                CreateDownloadDirectoryIfNotExists();

                await _client.DownloadMediaStreamAsync(streamInfo, path);

                return path;
            }
            catch
            {
                throw new YoutubeHandlerException(url, "Media stream could not be downloaded.");
            }
        }

        private void CreateDownloadDirectoryIfNotExists()
        {
            if(!Directory.Exists(_config.DownloadDirectory))
            {
                Directory.CreateDirectory(_config.DownloadDirectory);
            }
        }

        private YoutubeModel CreateModel(Video video, MediaStreamInfo info, string path)
        {
            try
            {
                return new YoutubeModel(video, info, path);
            }
            catch
            {
                throw new YoutubeHandlerException(video, info, "Model could not be instantiated.");
            }
        }
    }
}