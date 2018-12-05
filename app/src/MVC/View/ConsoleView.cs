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

            var input = Console.ReadLine();

            Controller.RequestDownload(input).Wait();

            Console.WriteLine("okie");
        }

        public void DownloadComplete()
        {
            Console.WriteLine("Download complete!");
        }
    }
}