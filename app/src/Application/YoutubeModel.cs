using YoutubeExtractorCore;

namespace YTTrimmer.Application
{
    public class YoutubeModel
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public VideoInfo Info { get; set; }
    }
}