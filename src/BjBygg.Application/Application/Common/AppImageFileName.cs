using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BjBygg.Application.Application.Common
{
    public class AppImageFileName
    {
        private string _fileName;

        public AppImageFileName(Stream stream, string fileExtension)
        {
            using (var file = new Bitmap(stream))
            {
                double ratio = Math.Round((double) file.Width / (double) file.Height, 2);
                _fileName = string.Format("{0}_{1}{2}", Guid.NewGuid(), ratio, fileExtension);
                stream.Position = 0;
            }
        }

        public override string ToString()
        {
            return _fileName;
        }
    }
}
