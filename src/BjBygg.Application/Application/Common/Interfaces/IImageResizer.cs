using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IImageResizer
    {
        BasicFileStream ResizeImage(BasicFileStream stream, int width);
    }
}
