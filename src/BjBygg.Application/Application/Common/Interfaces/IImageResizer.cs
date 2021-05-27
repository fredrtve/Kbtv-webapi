using BjBygg.SharedKernel;
using System.IO;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IImageResizer
    {
        void ResizeImage(Stream stream, Stream outputStream, string extension, int width = 0, int maxWidth = 0);
    }
}
