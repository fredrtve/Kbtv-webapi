using CleanArchitecture.Core;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.ExportCsvCommands
{
    public class ExportJsonToCsvCommandHandler : IRequestHandler<ExportJsonToCsvCommand, Uri>
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ICsvConverter _csvConverter;

        public ExportJsonToCsvCommandHandler(
            IBlobStorageService blobStorageService,
            ICsvConverter csvConverter
        ){
            _blobStorageService = blobStorageService;
            _csvConverter = csvConverter;
        }

        public async Task<Uri> Handle(ExportJsonToCsvCommand request, CancellationToken cancellationToken)
        {
            var csvString = _csvConverter.ConvertJsonListToCsv(request.JsonObjects, request.PropertyMap);

            // convert string to stream
            byte[] byteArray = Encoding.Default.GetBytes(csvString);
            Uri fileUrl;
            using(var stream = new BasicFileStream(new MemoryStream(byteArray), ".csv"))
            {
                fileUrl = await _blobStorageService.UploadFileAsync(stream, ResourceFolderConstants.CsvTemp);
            }

            return fileUrl;
        }
    }
}
