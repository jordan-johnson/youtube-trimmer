using System;

namespace YTTrimmer.MVC
{
    public class ConsoleView : IView
    {
        public IController Controller { get;set; }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void OnReady()
        {
            Console.WriteLine("Please enter the URL or Id of a Youtube video:");

            var videoLink = Console.ReadLine();

            Controller.RequestDownload(videoLink).Wait();

            Console.WriteLine("Download finished!");
        }

        public void DownloadComplete()
        {
            Console.WriteLine("Download complete!");
        }
    }
}