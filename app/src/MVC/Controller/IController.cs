using System.Threading.Tasks;

namespace YTTrimmer.MVC.Controller
{
    public interface IController
    {
        void RequestDownload(string url);
        void RequestTrim(string path, dynamic start, dynamic end);
    }
}