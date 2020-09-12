using System;
using System.IO;

namespace CleanArchitecture.SharedKernel
{
    public class BasicFileStream : IDisposable
    {
        public BasicFileStream(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
            FileExtension = Path.GetExtension(fileName);
            FileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
        }
        public BasicFileStream(byte[] bytes, string fileName)
        {
            Stream = new MemoryStream(bytes);
            FileName = fileName;
            FileExtension = Path.GetExtension(fileName);
            FileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);
        }

        public Stream Stream { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public string FileNameNoExtension { get; set; }

        public void Dispose() => Stream.Dispose();

    }
}
