using Newtonsoft.Json;
namespace YTTrimmer.Application
{
    public struct ConfigModel
    {
        [JsonIgnore]
        private string _downloadDirectory;

        public string FFMpegDirectory { get; set; }
        public string DownloadDirectory
        {
            get
            {
                return _downloadDirectory;
            }
            set
            {
                // add trailing slash if not found
                _downloadDirectory = (value.EndsWith("/")) ? value : value + "/";
            }
        }
        public string OutputFileNameTemplate { get; set; }
    }
}