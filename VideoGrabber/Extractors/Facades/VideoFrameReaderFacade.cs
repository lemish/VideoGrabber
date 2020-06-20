using GleamTech.VideoUltimate;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using VideoGrabber.Models;

namespace VideoGrabber.Extractors.Facades
{
    public class VideoFrameReaderFacade
    {
        private readonly VideoFrameReader _videoFrameReader;
        private readonly ImageFormat _imageFormat;

        public VideoFrameReaderFacade(Stream stream, ImageFormat imageFormat)
        {
            _imageFormat = imageFormat;

            stream.Position = 0;
            _videoFrameReader = new VideoFrameReader(stream);
        }

        public TimeSpan VideoDuration => _videoFrameReader.Duration;

        public MediaFile ExtractFrameByTimePosition(TimeSpan position, string fileName)
        {
            _videoFrameReader.Seek(position.TotalSeconds);

            if (!_videoFrameReader.Read())
                return null;

            var bitmapFrame = _videoFrameReader.GetFrame();

            return GetMediaFileFromBitmap(bitmapFrame, fileName);
        }

        private MediaFile GetMediaFileFromBitmap(Bitmap bitmap, string fileName)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, _imageFormat);
                var imageFormat = _imageFormat.ToString().ToLower();

                return new MediaFile(memoryStream, fileName, imageFormat);
            };
        }
    }
}
