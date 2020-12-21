using System.IO;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IFileZipper
    {
        Task ZipAsync(Stream output, string[] fileNames, string folder);
    }
}
