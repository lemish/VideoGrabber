using System;
using System.Drawing.Imaging;
using System.Threading.Tasks;

using VideoGrabber.Extractors;
using VideoGrabber.Storage;

namespace VideoGrabber
{
    public class Program
    {
        private static void Main(string[] args)
        {
        }

        private static async Task ExtractAudio()
        {
            var azureStorage = new AzureStorage();

            var currentBlobName = "test.mp4";
            var videoStream = await azureStorage.GetVideoStreamAsync(currentBlobName);

            var fileName = "test.wav";
            var audioFile = await AudioExtractor.ExtractWaveAudioAsync(videoStream, fileName);

            await azureStorage.UploadAudioAsync(audioFile);
        }

        private static async Task ExtractFrames()
        {
            var azureStorage = new AzureStorage();

            var currentBlobName = "test.mp4";
            var videoStream = await azureStorage.GetVideoStreamAsync(currentBlobName);

            var timeStep = new TimeSpan(hours: 0, minutes: 0, seconds: 3);
            var frames = VideoFrameExtractor.ExtractFramesByTimeStep(videoStream, ImageFormat.Png, timeStep);

            Parallel.ForEach(frames, async frame => await azureStorage.UploadFrameAsync(frame));
        }
    }
}
