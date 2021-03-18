using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Api.FileStorage
{
    public class AzureBlobConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private CloudBlobClient _blobClient;

        public AzureBlobConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CloudBlobContainer> GetBlobContainer(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentException("Configuration must contain ContainerName");
            }

            var blobClient = GetClient();

            var blobContainer = blobClient.GetContainerReference(folder);
            if (await blobContainer.CreateIfNotExistsAsync())
            {
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return blobContainer;
        }

        private CloudBlobClient GetClient()
        {
            if (_blobClient != null)
                return _blobClient;

            var storageConnectionString = _configuration.GetValue<string>("BlobStorageConnectionString");
            if (string.IsNullOrWhiteSpace(storageConnectionString))
            {
                throw new ArgumentException("Configuration must contain StorageConnectionString");
            }

            if (!CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                throw new Exception("Could not create storage account with StorageConnectionString configuration");
            }

            _blobClient = storageAccount.CreateCloudBlobClient();
            return _blobClient;
        }
    }
}
