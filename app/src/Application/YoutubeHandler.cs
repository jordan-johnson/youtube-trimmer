using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExtractorCore;

namespace YTTrimmer.Application
{
    public class YoutubeHandler
    {
        private WebRequest _web;
        private YoutubeModel _model;

        public YoutubeHandler(WebRequest web)
        {
            _web = web;
        }

        public string GetVideoIdFromYoutubeURL(string url)
        {
            return url.Split("=").Last();
        }

        public async Task<YoutubeModel> DownloadBestResolution(string url)
        {
            _model = new YoutubeModel() { Id = GetVideoIdFromYoutubeURL(url) };

            var metadata = await GetVideoMetadata();
            _model.Info = GetVideoByBestResolution(metadata);
            _model.Path = await Download();

            return _model;
        }

        private async Task<string> Download()
        {
            string filename = _model.Id + _model.Info.VideoExtension;

            return await _web.Download(_model.Info.DownloadUrl, filename);
        }

        public async Task<IEnumerable<VideoInfo>> GetVideoMetadata()
        {
            Func<VideoInfo, bool> condition = x => x.Resolution > 0 && x.AudioBitrate > 0;
            IEnumerable<VideoInfo> metadata = null;

            metadata = await DownloadUrlResolver.GetVideoUrlsAsync(_model.Id, condition, false);

            return metadata;
        }

        private VideoInfo GetVideoByBestResolution(IEnumerable<VideoInfo> videos)
        {
            int bestResolution = videos.Max(x => x.Resolution);

            return videos.FirstOrDefault(x => x.Resolution == bestResolution);
        }
    }
}