using System;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;
using MediaToolkit.Model;

namespace YTTrimmer.Exception
{
    public class TrimmerException : System.Exception
    {
        public TrimVideoModel VideoModel { get; }

        public TrimmerException(string message)
            : base(message)
        {
        }

        public TrimmerException(TrimVideoModel model, string message)
            : base(message)
        {
            VideoModel = model;
        }
    }
}