namespace YTTrimmer.MVC
{
    public interface IView
    {
        IController Controller { get; set; }

        void Close();
        void OnReady();
        void DownloadComplete();
    }
}