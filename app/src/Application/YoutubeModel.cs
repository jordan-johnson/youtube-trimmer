using System;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YTTrimmer.Application
{
    public class YoutubeModel
    {
        public string Id { get; }
        public string Title { get; }
        public string Author { get; }
        public TimeSpan Duration { get; }
        public DateTimeOffset UploadDate { get; }
        public string Extension { get; }
        public string Path { get; }

        public YoutubeModel(Video metadata, MediaStreamInfo streamInfo, string path)
        {
            Id = metadata.Id;
            Title = metadata.Title;
            Author = metadata.Author;
            Duration = metadata.Duration;
            UploadDate = metadata.UploadDate;
            Extension = streamInfo.Container.GetFileExtension();
            Path = path;
        }
    }
}