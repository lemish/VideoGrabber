using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

using VideoGrabber.Extractors.Facades;
using VideoGrabber.Models;

namespace VideoGrabber.Extractors
{
    public static class VideoFrameExtractor
    {
        public static List<MediaFile> ExtractFramesByTimeStep(Stream stream,
                                                              ImageFormat outputFormat,
                                                              TimeSpan step)
        {
            var videoFrameReaderFacade = new VideoFrameReaderFacade(stream, outputFormat);

            var durationInSeconds = videoFrameReaderFacade.VideoDuration.TotalSeconds;
            var stepsCount = (int)(durationInSeconds / step.Seconds);
            var frames = new List<MediaFile>(stepsCount);

            for (var currentPosition = 0; durationInSeconds >= currentPosition; currentPosition += step.Seconds)
            {
                var framePosition = new TimeSpan(hours: 0, minutes: 0, seconds: currentPosition);
                var frameName = currentPosition.ToString("F") + "s";

                var frame = videoFrameReaderFacade.ExtractFrameByTimePosition(framePosition, frameName);

                frames.Add(frame);
            }

            return frames;
        }
    }
}
