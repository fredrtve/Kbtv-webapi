using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Api.FileStorage
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly AzureBlobConnectionFactory _azureBlobConnectionFactory;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            _azureBlobConnectionFactory = new AzureBlobConnectionFactory(configuration);
        }

        public async Task DeleteAsync(string fileUri)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

            Uri uri = new Uri(fileUri);
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<Uri>> ListAsync()
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
            var allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);
            return allBlobs;
        }

        public async Task<IEnumerable<Uri>> UploadAsync(IFormFileCollection files)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
            var blobs = new List<Uri>();
            for (int i = 0; i < files.Count; i++)
            {
                var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(files[i].FileName));
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
                blobs.Add(blob.Uri);
            }
            return blobs;
        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
