using NAudio.Wave;

using System.IO;
using System.Threading.Tasks;

using VideoGrabber.Models;

namespace VideoGrabber.Extractors
{
    public static class AudioExtractor
    {
        public static async Task<MediaFile> ExtractWaveAudioAsync(Stream stream,
                                                                  string fileName)
        {
            var reader = new StreamMediaFoundationReader(stream);
            var pcmStream = WaveFormatConversionStream.CreatePcmStream(reader);

            var memoryStream = new MemoryStream();

            using (var writer = new WaveFileWriter(memoryStream, reader.WaveFormat))
            {
                await pcmStream.CopyToAsync(writer);
            }

            const string waveExtension = "wav";

            return new MediaFile(memoryStream, fileName, waveExtension);
        }
    }
}
