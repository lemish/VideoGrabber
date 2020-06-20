using SystemConfigurations = System.Configuration.ConfigurationManager;

namespace VideoGrabber
{
    public static class ConfigurationManager
    {
        public static string AzureStorageConnectionString => SystemConfigurations.AppSettings["AzureStorageConnectionString"];
    }
}
