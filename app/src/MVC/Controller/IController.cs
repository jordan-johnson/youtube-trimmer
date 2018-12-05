using System.Threading.Tasks;

namespace YTTrimmer.MVC
{
    public interface IController
    {
        Task RequestDownload(string url);
    }
}