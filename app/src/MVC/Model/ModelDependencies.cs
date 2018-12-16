using YTTrimmer.Application;
using YTTrimmer.Application.Models;

namespace YTTrimmer.MVC.Model
{
    public class ModelDependencies
    {
        protected ConfigModel Config;
        protected YoutubeHandler Youtube;
        protected Trimmer TrimTool;

        public virtual void Setup(ConfigModel config, YoutubeHandler youtube, Trimmer trimmer)
        {
            Config = config;
            Youtube = youtube;
            TrimTool = trimmer;
        }
    }
}