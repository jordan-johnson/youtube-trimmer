using System;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YTTrimmer.Exception
{
    public class YoutubeHandlerException : System.Exception
    {
        public string Address { get; }
        public Video Video { get; }
        public MediaStreamInfo StreamInfo { get; }

        public YoutubeHandlerException(string message)
            : base(message)
        {
        }

        public YoutubeHandlerException(string url, string message)
            : base(message)
        {
            Address = url;
        }

        public YoutubeHandlerException(Video video, MediaStreamInfo streamInfo, string message)
        {
            Video = video;
            StreamInfo = streamInfo;
        }

        public YoutubeHandlerException(System.Exception inner, string message)
            : base(message, inner)
        {
        }
    }
}