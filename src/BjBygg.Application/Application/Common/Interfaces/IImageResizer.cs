using BjBygg.SharedKernel;
using System.IO;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IImageResizer
    {
        Stream ResizeImage(Stream stream, string extension, int width = 0, int maxWidth = 0);
    }
}
