using BjBygg.Application.Application.Common.Interfaces;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Api.FileStorage
{
    public class AzureBlobStorageZipper : IFileZipper
    {
        private readonly AzureBlobConnectionFactory _azureBlobConnectionFactory;

        public AzureBlobStorageZipper(IConfiguration configuration)
        {
            _azureBlobConnectionFactory = new AzureBlobConnectionFactory(configuration);
        }

        public async Task ZipAsync(Stream output, string[] fileNames, string folder)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer(folder);

            var zipOutputStream = new ZipOutputStream(output);

            foreach (var fileName in fileNames)
            {
                zipOutputStream.SetLevel(0);
                var blob = blobContainer.GetBlockBlobReference(fileName);
                var entry = new ZipEntry(fileName);
                zipOutputStream.PutNextEntry(entry);
                await blob.DownloadToStreamAsync(zipOutputStream);
            }
            zipOutputStream.IsStreamOwner = false;
            zipOutputStream.Finish();
            zipOutputStream.Close();

            output.Position = 0;
        }
    }
}
