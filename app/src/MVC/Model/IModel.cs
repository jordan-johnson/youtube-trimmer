using System.Threading.Tasks;
using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.MVC.Model
{
    public interface IModel
    {
        void Setup(ConfigModel config, YoutubeHandler youtube, Trimmer trimmer);
        void Download(string url);
        void Trim(string path, dynamic start, dynamic end);
    }
}