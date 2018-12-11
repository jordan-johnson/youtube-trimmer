using System;
using System.IO;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YTTrimmer.Application
{
    public class YoutubeModel
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTimeOffset UploadDate { get; private set; }
        public string Path { get; private set; }
        public double DownloadProgress { get; set; }
        public MediaStreamInfo MediaStreamInfo { get; private set; }

        public string FileName
        {
            get
            {
                return String.Format("{0}.{1}", Id, Extension);
            }
        }

        public string Extension
        {
            get
            {
                return MediaStreamInfo.Container.GetFileExtension();
            }
        }

        public bool FileExists
        {
            get
            {
                return File.Exists(Path);
            }
        }

        public YoutubeModel(string id)
        {
            Id = id;
        }

        public void ApplyMetadata(Video metadata)
        {
            Id = metadata.Id;
            Title = metadata.Title;
            Author = metadata.Author;
            Duration = metadata.Duration;
            UploadDate = metadata.UploadDate;
        }

        public void ApplyMediaStreamInfo(MediaStreamInfo info)
        {
            MediaStreamInfo = info;
        }

        public void ApplyPath(string path)
        {
            Path = path;
        }
    }
}