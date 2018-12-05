using System.Threading.Tasks;

namespace YTTrimmer.MVC
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

        public async Task RequestDownload(string url)
        {
            if(url == "exit" || url == "quit")
                _view.Close();

            await _model.Download(url);
            _view.DownloadComplete();
            _view.OnReady();
        }
    }
}