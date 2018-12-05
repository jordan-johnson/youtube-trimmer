using System;

namespace YTTrimmer.Exception
{
    public class ConfigSerializationException : System.Exception
    {
        public string Location { get; }

        public ConfigSerializationException(string message)
            : base(message)
        {
        }

        public ConfigSerializationException(string path, string message)
            : base(message)
        {
            Location = path;
        }
    }
}