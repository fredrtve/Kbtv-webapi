using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IImageResizer
    {
        BasicFileStream ResizeImage(BasicFileStream stream, int width = 0, int maxWidth = 0);
    }
}
