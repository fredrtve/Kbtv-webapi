using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Services;
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

        public async Task DeleteAsync(string fileUri, FileType fileType = FileType.Image)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(fileType);

            Uri uri = new Uri(fileUri);
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<Uri>> ListAsync(FileType fileType = FileType.Image)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(fileType);
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

        public async Task<IEnumerable<Uri>> UploadFilesAsync(IFormFileCollection files, FileType fileType = FileType.Image)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(fileType);
            var blobs = new List<Uri>();
            for (int i = 0; i < files.Count; i++)
            {
                var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(Path.GetExtension(files[i].FileName)));
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
                blobs.Add(blob.Uri);
            }
            return blobs;
        }

        public async Task<Uri> UploadFileAsync(IFormFile file, FileType fileType = FileType.Image)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(fileType);

            var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(Path.GetExtension(file.FileName)));
                using (var stream = file.OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }   
            return blob.Uri;
        }
        public async Task<Uri> UploadFileAsync(Stream stream, string extension, FileType fileType = FileType.Image)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(fileType);

            var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(extension));
            using (var st = stream)
            {
                await blob.UploadFromStreamAsync(st);
            }
            return blob.Uri;
        }

        private string GetRandomBlobName(string extension)
        {
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), extension);
        }
}
}
