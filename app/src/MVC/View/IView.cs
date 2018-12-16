using YTTrimmer.MVC.Controller;

namespace YTTrimmer.MVC.View
{
    public interface IView
    {
        IController Controller { get; set; }

        void OnReady();
    }
}