using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.StaticFiles;
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

        public async Task<IEnumerable<Uri>> UploadFilesAsync(DisposableList<BasicFileStream> streams, string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);
            var blobs = new List<Uri>();
            for (int i = 0; i < streams.Count; i++)
            {
                var fileStream = streams[i];
                var blob = blobContainer.GetBlockBlobReference(fileStream.FileName);

                await blob.UploadFromStreamAsync(fileStream.Stream);

                //Content disposition not working? Wont download file for client
                //blob.Properties.ContentType = GetContentType(fileStream.FileExtension);
                //blob.Properties.ContentDisposition = "attachment; filename=" + fileStream.FileName;              
                //await blob.SetPropertiesAsync(); //Unneccesary?

                blobs.Add(blob.Uri);
            }
            return blobs;
        }

        public async Task<Uri> UploadFileAsync(BasicFileStream stream, string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);

            var blob = blobContainer.GetBlockBlobReference(stream.FileName);

            await blob.UploadFromStreamAsync(stream.Stream);

            //Content disposition not working? Wont download file for client
            //blob.Properties.ContentType = GetContentType(stream.FileExtension);
            //blob.Properties.ContentDisposition = "attachment; filename=" + stream.FileName;
            //await blob.SetPropertiesAsync(); //Unneccesary?
            
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
