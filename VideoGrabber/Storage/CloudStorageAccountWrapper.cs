using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace VideoGrabber.Storage
{
    public static class CloudStorageAccountWrapper
    {
        public static CloudBlobClient GetBlobClient()
        {
            var connectionString = ConfigurationManager.AzureStorageConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            return storageAccount.CreateCloudBlobClient();
        }
    }
}
