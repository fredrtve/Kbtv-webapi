using CleanArchitecture.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<Uri>> ListAsync(FileType fileType = FileType.Image);
        Task<IEnumerable<Uri>> UploadFilesAsync(IFormFileCollection files, FileType fileType = FileType.Image);
        Task<Uri> UploadFileAsync(Stream stream, string extension, FileType fileType = FileType.Image);
        Task<Uri> UploadFileAsync(IFormFile file, FileType fileType = FileType.Image);
        Task DeleteAsync(string fileUri, FileType fileType = FileType.Image);
    }
}
