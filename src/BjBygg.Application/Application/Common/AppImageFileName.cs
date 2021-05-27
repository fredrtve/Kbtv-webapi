using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            using (var file = Image.FromStream(stream, false, false))
            {
                FixOrientation(file);
                double ratio = Math.Round((double) file.Width / (double) file.Height, 2);
                _fileName = string.Format("{0}_{1}{2}{3}", Guid.NewGuid(), "ratio=", ratio, fileExtension);
                stream.Position = 0;
            }
        }

        public override string ToString()
        {
            return _fileName;
        }

        private void FixOrientation(Image img)
        {
            if (Array.IndexOf(img.PropertyIdList, 274) > -1)
            {
                var orientation = (int)img.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;
                    case 2:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        img.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        img.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                // This EXIF data is now invalid and should be removed.
                img.RemovePropertyItem(274);
            }
        }
    }
}
