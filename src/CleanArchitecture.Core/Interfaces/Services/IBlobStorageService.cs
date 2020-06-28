using CleanArchitecture.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<Uri>> ListAsync(string folder);
        Task<IEnumerable<Uri>> UploadFilesAsync(IFormFileCollection files, string folder);
        Task<Uri> UploadFileAsync(Stream stream, string extension, string folder);
        Task<Uri> UploadFileAsync(IFormFile file, string folder);
        Task DeleteAsync(string fileUri, string folder);
    }
}
