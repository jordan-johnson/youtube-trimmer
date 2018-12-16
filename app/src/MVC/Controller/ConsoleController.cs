using System.Threading.Tasks;
using YTTrimmer.MVC.Model;
using YTTrimmer.MVC.View;

namespace YTTrimmer.MVC.Controller
{
    public class ConsoleController : IController
    {
        private IModel _model;
        private IView _view;

        public ConsoleController(IModel model, IView view)
        {
            _model = model;
            _view = view;
            
            _view.Controller = this;
        }

        public void RequestDownload(string url)
        {
            _model.Download(url);
            _view.OnReady();
        }

        public void RequestTrim(string path, dynamic start, dynamic end)
        {
            _model.Trim(path, start, end);
            _view.OnReady();
        }
    }
}