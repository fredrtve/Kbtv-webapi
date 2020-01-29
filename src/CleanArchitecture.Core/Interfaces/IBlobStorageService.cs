using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<Uri>> ListAsync(string fileType = "image");
        Task<IEnumerable<Uri>> UploadFilesAsync(IFormFileCollection files, string fileType = "image");
        Task<Uri> UploadFileAsync(IFormFile file, string fileType = "image");
        Task DeleteAsync(string fileUri, string fileType = "image");
    }
}
