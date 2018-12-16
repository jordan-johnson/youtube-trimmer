using System.Threading.Tasks;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.MVC.Model
{
    public class ConsoleModel : ModelDependencies, IModel
    {
        public void Download(string url)
        {
            Youtube.ClearQueue();
            Youtube.Queue(url);
            Youtube.DownloadQueue().Wait();
        }

        public void Trim(string path, dynamic start, dynamic end)
        {
            TrimTool.Load(Config.DownloadDirectory + path);
            TrimTool.SetTrimSection(start, end);
            TrimTool.Run();
        }
    }
}