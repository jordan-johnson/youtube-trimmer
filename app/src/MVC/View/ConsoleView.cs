using System;
using YTTrimmer.MVC.Controller;
using YTTrimmer.MVC.Model;

namespace YTTrimmer.MVC.View
{
    public class ConsoleView : IView
    {
        private enum ActionType
        {
            None = -1,
            Download = 0,
            Trim = 1
        }

        public IController Controller { get;set; }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void OnReady()
        {
            var action = ChooseAction();

            switch(action)
            {
                case ActionType.None:
                    OnReady();
                break;
                case ActionType.Download:
                    RequestDownload();
                break;
                case ActionType.Trim:
                    RequestTrim();
                break;
            }
        }

        private ActionType ChooseAction()
        {
            Console.WriteLine("Enter next step: (0 = download YT video, 1 = trim video)");

            var input = Console.ReadLine();
            var parse = (ActionType)Enum.Parse(typeof(ActionType), input);

            if(!Enum.IsDefined(typeof(ActionType), parse))
                parse = ActionType.None;

            return parse;
        }

        private void RequestDownload()
        {
            Console.WriteLine("Please enter the URL or Id of a Youtube video:");
            
            var URL = Console.ReadLine();

            Controller.RequestDownload(URL);

            Console.WriteLine("Download complete.");
        }

        private void RequestTrim()
        {
            Console.WriteLine("Please enter file name in download directory:");

            var path = Console.ReadLine();

            Console.WriteLine("Please enter start trim time (i.e. 00:00:10 is 10 seconds):");

            var start = Console.ReadLine();

            Console.WriteLine("Please enter end trim time (i.e. 00:00:15 is 15 seconds):");

            var end = Console.ReadLine();

            Controller.RequestTrim(path, start, end);
        }
    }
}