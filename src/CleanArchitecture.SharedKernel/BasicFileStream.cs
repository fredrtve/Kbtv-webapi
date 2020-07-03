using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanArchitecture.SharedKernel
{
    public class BasicFileStream : IDisposable
    {
        public BasicFileStream(Stream stream, string fileExtension)
        {
            Stream = stream;
            FileExtension = fileExtension;
        }

        public Stream Stream { get; set; }

        public string FileExtension { get; set; }

        public void Dispose() => Stream.Dispose();
        
    }
}
