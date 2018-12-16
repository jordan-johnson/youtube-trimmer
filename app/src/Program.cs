using System;
using YTTrimmer.Application;
using YTTrimmer.MVC.Controller;
using YTTrimmer.MVC.Model;
using YTTrimmer.MVC.View;

namespace YTTrimmer
{
    public class Program
    {
        static void Main()
        {
            /**
             * Application tools
             */
            var config = new ConfigSerialization().Model;
            var youtube = new YoutubeHandler(config);
            var trimmer = new Trimmer(config);

            /**
             * MVC
             */
            var model = new ConsoleModel();
            var view = new ConsoleView();            
            var controller = new ConsoleController(model, view);

            model.Setup(config, youtube, trimmer);
            view.OnReady();
        }
    }
}