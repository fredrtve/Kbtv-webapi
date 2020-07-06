using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.ExportCsvCommands
{
    public class ExportJsonToCsvHandler : IRequestHandler<ExportJsonToCsvCommand, Uri>
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ICsvConverter _csvConverter;

        public ExportJsonToCsvHandler(
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
