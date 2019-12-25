using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<Uri>> ListAsync();
        Task<IEnumerable<Uri>> UploadAsync(IFormFileCollection files);
        Task DeleteAsync(string fileUri);
    }
}
