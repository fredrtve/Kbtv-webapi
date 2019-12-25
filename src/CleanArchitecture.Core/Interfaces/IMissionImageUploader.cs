using CleanArchitecture.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMissionImageUploader
    {
        Task<IEnumerable<MissionImage>> UploadCollection(IFormFileCollection files, int missionId);

        Task<bool> DeleteImage(int id);
    }
}
