using System.Threading.Tasks;
using YTTrimmer.Application;

namespace YTTrimmer.MVC
{
    public class ConsoleModel : IModel
    {
        public ConfigModel Config { get; private set; }
        public YoutubeHandler Youtube { get; private set; }

        public void Setup(ConfigModel config, YoutubeHandler youtube)
        {
            Config = config;
            Youtube = youtube;
        }

        public async Task Download(string input)
        {
            // todo
        }
    }
}