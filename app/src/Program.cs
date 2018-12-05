using System;
using YTTrimmer.Application;
using YTTrimmer.MVC;

namespace YTTrimmer
{
    public class Program
    {
        static void Main()
        {
            var config = new ConfigSerialization().Model;
            var youtube = new YoutubeHandler(config);

            var model = new ConsoleModel();
            model.Setup(config, youtube);
            
            var view = new ConsoleView();
            var controller = new ConsoleController(model, view);

            view.OnReady();
        }
    }
}