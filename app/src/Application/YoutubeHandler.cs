using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using YTTrimmer.Application.Models;
using YTTrimmer.Exception;

namespace YTTrimmer.Application
{
    public partial class YoutubeHandler
    {
        private ConfigModel _config;
        private YoutubeClient _client;
        private List<YoutubeVideoModel> _queue;

        public YoutubeHandler(ConfigModel config)
        {
            /**
             * Fixes WebException thrown "Decoded string is not a valid IDN name"
             * when attempting to download Youtube video files. 
             */
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            
            _config = config;
            _client = new YoutubeClient();
            _queue = new List<YoutubeVideoModel>();
        }

        public void Queue(string url)
        {
            var videoId = ParseVideoId(url);

            _queue.Add(new YoutubeVideoModel(videoId));
        }

        public void Queue(IEnumerable<string> urls)
        {
            foreach(var url in urls)
            {
                Queue(url);
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

        public List<YoutubeVideoModel> GetDownloadedFromQueue()
        {
            return _queue.FindAll(x => x.FileExists);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }

        private async Task DownloadVideoAsync(YoutubeVideoModel model)
        {
            await GetVideoMetadataAsync(model);
            await GetStreamInfoAsync(model);
            await DownloadVideoStreamAsync(model);
        }

        private async Task GetVideoMetadataAsync(YoutubeVideoModel model)
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

        private async Task GetStreamInfoAsync(YoutubeVideoModel model)
        {
            try
            {
                var streamInfoSet = await _client.GetVideoMediaStreamInfosAsync(model.Id);
                var muxedInfo = streamInfoSet.Muxed.WithHighestVideoQuality();

                model.MediaStreamInfo = muxedInfo;
            }
            catch
            {
                throw new YoutubeHandlerException(model.Id, "Could not get media stream info set.");
            }
        }

        private async Task DownloadVideoStreamAsync(YoutubeVideoModel model)
        {
            try
            {
                var progress = new Progress<double>(x => model.DownloadProgress = x);

                model.Path = _config.DownloadDirectory + model.FileName;

                await _client.DownloadMediaStreamAsync(model.MediaStreamInfo, model.Path, progress);
            }
            catch
            {
                throw new YoutubeHandlerException(model.Id, "Media stream could not be downloaded.");
            }
        }
    }
}