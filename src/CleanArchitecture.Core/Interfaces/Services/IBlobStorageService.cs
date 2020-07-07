using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<Uri>> ListAsync(string folder);
        Task<IEnumerable<Uri>> UploadFilesAsync(DisposableList<BasicFileStream> streams, string folder);
        Task<Uri> UploadFileAsync(BasicFileStream stream, string folder);
        Task DeleteAsync(string fileUri, string folder);
    }
}
