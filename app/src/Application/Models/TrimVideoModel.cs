using System;
using System.IO;
using System.Linq;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace YTTrimmer.Application.Models
{
    public class TrimVideoModel : IVideoModel
    {
        public MediaFile Input { get; private set; }
        public MediaFile Output { get; private set; }
        public TimeDataModel TrimTime { get; private set; }

        public string FileName
        {
            get
            {
                // the MediaFile filename is the full path
                // so this will return the actual filename
                // i.e. "example.mp4"
                return Input.Filename.Split("/").Last();
            }
        }

        public string Extension
        {
            get
            {
                return FileName.Split(".").Last();
            }
        }

        public string Path
        {
            get
            {
                return Input.Filename;
            }
        }

        public bool FileExists
        {
            get
            {
                return File.Exists(Path);
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return Input.Metadata.Duration;
            }
        }

        public TrimVideoModel(string filename)
        {
            Input = new MediaFile {Filename = filename};
        }

        public void SetTrimTime(dynamic start, dynamic end)
        {
            TrimTime = new TimeDataModel(start, end);
        }

        public void CreateOutput(string directory, string fileNameTemplate)
        {
            var fileNameSplit = FileName.Split(".");
            var outputName = string.Format(fileNameTemplate, fileNameSplit[0], fileNameSplit[1]);
            var outputPath = directory + outputName;

            Output = new MediaFile {Filename = outputPath};
        }
    }
}