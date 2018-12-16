using Newtonsoft.Json;
namespace YTTrimmer.Application.Models
{
    public struct ConfigModel
    {
        [JsonIgnore]
        private string _downloadDirectory;
        
        public string FFMpegDirectory { get; set; }
        public string OutputFileNameTemplate { get; set; }

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
    }
}