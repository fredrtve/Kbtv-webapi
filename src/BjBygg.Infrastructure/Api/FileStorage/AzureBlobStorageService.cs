using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.SharedKernel;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Api.FileStorage
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly AzureBlobConnectionFactory _azureBlobConnectionFactory;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            _azureBlobConnectionFactory = new AzureBlobConnectionFactory(configuration);
        }

        public async Task DeleteAsync(string fileUri, string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);

            Uri uri = new Uri(fileUri);
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<Uri>> ListAsync(string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);
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
        public async Task GetAsync(string fileName, string folder, Stream target)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);
            var blob = blobContainer.GetBlockBlobReference(fileName);
            await blob.DownloadToStreamAsync(target);
        }

        public async Task<Uri> UploadFileAsync(Stream stream, string fileName, string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);

            var blob = blobContainer.GetBlockBlobReference(fileName);

            await blob.UploadFromStreamAsync(stream);

            //Content disposition not working? Wont download file for client
            blob.Properties.ContentType = GetContentType(Path.GetExtension(fileName));
            blob.Properties.ContentDisposition = "attachment";
            await blob.SetPropertiesAsync(); 

            return blob.Uri;
        }

        private string GetContentType(string extension)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(extension, out contentType);
            return contentType;
        }

        private string GetRandomBlobName(string extension)
        {
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), extension);
        }
    }
}
