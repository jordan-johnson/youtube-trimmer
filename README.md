# youtube-trimmer

This application allows you to download Youtube videos, and it also includes video-trimming functionality. You can use either tool without the other.

## Dependencies

>NOTE: This project was written using .NET Core 2.1. Using another version is unsupported but may work. .NET Core 2.0 seems to work fine.

### Not included in build

* .NET Core 2.1
* ffmpeg

### Included in build

* AngleSharp (used by YoutubeExplode)
* MediaToolkit
* Newtonsoft.Json
* YoutubeExplode


## Initial Setup

In `build/1.0/` of this repository, simply run the command `dotnet app.dll` in the terminal. This will launch the application.

>NOTE: In order for the trim tool to work, you must have ffmpeg installed on your machine.

On application launch, a `config.json` file is generated in the root directory as well as a `downloads/` directory. The config file has default values. Change these to match your preferences, and most importantly, change the `FFMpegDirectory` variable to the correct path. Update these values if necessary and restart the application.

The default configuration:

``` json
{
  "FFMpegDirectory": "/usr/local/bin/ffmpeg",
  "OutputFileNameTemplate": "{0}_trimmed.{1}",
  "DownloadDirectory": "downloads/"
}
```

## How to Use

The Console will ask which tool you wish to use.

### Youtube Tool

Simply enter either a Youtube URL or the video id. The video will begin downloading immediately.

### Trimmer Tool

Enter in the name of the video file in the downloads directory. Next, you'll be asked to set the start and end times that you wish to trim.

The times MUST be in a format of `00:00:00` representing hours:minutes:seconds.

## What's Next

It may be some time before I update this project; but if I do, I'm looking to add:

* Cross-platform GUI (this will enable the already built asynchronous downloading functionality)
* Progress display for both the Youtube and Trimmer tools.
