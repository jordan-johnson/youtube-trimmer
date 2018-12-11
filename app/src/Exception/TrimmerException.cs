using System;
using YTTrimmer.Application;
using MediaToolkit.Model;

namespace YTTrimmer.Exception
{
    public class TrimmerException : System.Exception
    {
        public string Path { get; set; }
        public TimeDataModel TimeData { get; set; }

        public TrimmerException(string message)
            : base(message)
        {
        }

        public TrimmerException(string path, string message)
            : base(message)
        {
            Path = path;
        }

        public TrimmerException(MediaFile input, TimeDataModel timeData, string message) 
            : base(message)
        {
            Path = input.Filename;
            TimeData = timeData;
        }

        public TrimmerException(string path, string start, string end, string message)
            : base(message)
        {
            Path = path;
            TimeData = new TimeDataModel(start, end);
        }
    }
}