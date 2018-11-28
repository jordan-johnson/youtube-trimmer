namespace YTTrimmer.Application
{
    public struct ConfigModel
    {
        public string FFMpegDirectory { get; set; }
        public string DownloadDirectory { get; set; }
        public string OutputFileNameTemplate { get; set; }
    }
}