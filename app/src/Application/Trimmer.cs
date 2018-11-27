using System;
using System.IO;
using System.Linq;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace YTTrimmer.Application
{
    public class Trimmer
    {
        public static void Run(string dir, string filename, uint start, uint span)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            string directoryPath = dirInfo.FullName + "/" + dir;

            var input = new MediaFile {Filename = @directoryPath + filename};
            var fileNameSplit = filename.Split(".");
            var outputFileName = fileNameSplit[0] + "_trimmed." + fileNameSplit[1];
            var output = new MediaFile {Filename = @directoryPath + outputFileName};

            using(Engine engine = new Engine("/usr/local/bin/ffmpeg"))
            {
                engine.GetMetadata(input);

                ConversionOptions options = new ConversionOptions();
                options.CutMedia(TimeSpan.FromSeconds(start), TimeSpan.FromSeconds(span));

                engine.Convert(input, output, options);
            }
        }
    }
}