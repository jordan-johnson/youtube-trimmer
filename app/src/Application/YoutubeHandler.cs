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
    public partial class YoutubeHandler
    {
        private ConfigModel _config;
        private YoutubeClient _client;
        private List<YoutubeModel> _queue;

        public YoutubeHandler(ConfigModel config)
        {
            /**
             * Fixes WebException thrown "Decoded string is not a valid IDN name"
             * when attempting to download Youtube video files. 
             */
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            
            _config = config;
            _client = new YoutubeClient();
            _queue = new List<YoutubeModel>();
        }

        public void QueueVideo(string url)
        {
            var videoId = ParseVideoId(url);

            _queue.Add(new YoutubeModel(videoId));
        }

        public void QueueVideos(IEnumerable<string> urls)
        {
            foreach(var url in urls)
            {
                QueueVideo(url);
            }
        }

        public async Task DownloadQueue()
        {
            var tasks = new List<Task>();

            foreach(var model in _queue)
            {
                var task = DownloadVideoAsync(model);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public List<YoutubeModel> GetDownloadedFromQueue()
        {
            return _queue.FindAll(x => x.FileExists);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }

        private async Task DownloadVideoAsync(YoutubeModel model)
        {
            await GetVideoMetadataAsync(model);
            await GetStreamInfoAsync(model);
            await DownloadVideoStreamAsync(model);
        }

        private async Task GetVideoMetadataAsync(YoutubeModel model)
        {
            try
            {
                var video = await _client.GetVideoAsync(model.Id);

                model.ApplyMetadata(video);
            }
            catch
            {
                throw new YoutubeHandlerException(model.Id, "Could not pull metadata.");
            }
        }

        private async Task GetStreamInfoAsync(YoutubeModel model)
        {
            try
            {
                var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(model.Id);
                var muxedInfo = streamInfoSet.Muxed.WithHighestVideoQuality();

                model.ApplyMediaStreamInfo(muxedInfo);
            }
            catch
            {
                throw new YoutubeHandlerException(model.Id, "Could not get media stream info set.");
            }
        }

        private async Task DownloadVideoStreamAsync(YoutubeModel model)
        {
            try
            {
                var path = _config.DownloadDirectory + model.FileName;
                var progress = new Progress<double>(x => model.DownloadProgress = x);

                model.ApplyPath(path);

                CreateDownloadDirectoryIfNotExists();

                await _client.DownloadMediaStreamAsync(model.MediaStreamInfo, path, progress);
            }
            catch
            {
                throw new YoutubeHandlerException(model.Id, "Media stream could not be downloaded.");
            }
        }
    }
}