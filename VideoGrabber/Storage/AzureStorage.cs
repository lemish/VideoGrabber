using Microsoft.WindowsAzure.Storage.Blob;

using System.IO;
using System.Threading.Tasks;

using VideoGrabber.Models;
using VideoGrabber.Storage.Constants;

namespace VideoGrabber.Storage
{
    public class AzureStorage
    {
        private readonly CloudBlobContainer _audiosContainer;
        private readonly CloudBlobContainer _framesContainer;
        private readonly CloudBlobContainer _videosContainer;

        public AzureStorage()
        {
            var blobClient = CloudStorageAccountWrapper.GetBlobClient();

            _audiosContainer = blobClient.GetContainerReference(ContainerNames.Audios);
            _framesContainer = blobClient.GetContainerReference(ContainerNames.Frames);
            _videosContainer = blobClient.GetContainerReference(ContainerNames.Videos);
        }

        public async Task<Stream> GetVideoStreamAsync(string blobName)
        {
            var blockBlob = _videosContainer.GetBlockBlobReference(blobName);

            var memoryStream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(memoryStream);

            return memoryStream;
        }

        public Task UploadAudioAsync(MediaFile audioFile)
        {
            var blockBlob = _audiosContainer.GetBlockBlobReference(audioFile.Name);

            return blockBlob.UploadFromByteArrayAsync(audioFile.Content, 0, audioFile.Content.Length);
        }

        public Task UploadFrameAsync(MediaFile frameFile)
        {
            var blockBlob = _framesContainer.GetBlockBlobReference(frameFile.Name);

            return blockBlob.UploadFromByteArrayAsync(frameFile.Content, 0, frameFile.Content.Length);
        }
    }
}
