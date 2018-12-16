using System;

namespace YTTrimmer.Application.Models
{
    public interface IVideoModel
    {
        string FileName { get; }
        string Extension { get; }
        string Path { get; }
        bool FileExists { get; }
        TimeSpan Duration { get; }
    }
}