using System.Threading.Tasks;
using YTTrimmer.Application;

namespace YTTrimmer.MVC
{
    public interface IModel
    {
        ConfigModel Config { get; }
        YoutubeHandler Youtube { get; }

        void Setup(ConfigModel config, YoutubeHandler youtube);
        Task Download(string input);
    }
}