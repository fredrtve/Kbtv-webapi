using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Infrastructure.Services
{
    public class ImageResizer : IImageResizer
    {
        private IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;

            extension = extension.Replace(".", "");

            var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", options: RegexOptions.IgnoreCase);

            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;

                    case "jpg":
                        encoder = new JpegEncoder();
                        break;

                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;

                    case "gif":
                        encoder = new GifEncoder();
                        break;

                    default:
                        break;
                }
            }

            return encoder;
        }

        public BasicFileStream ResizeImage(BasicFileStream input, int width = 0, int maxWidth = 0)
        {
            if (input == null) throw new ArgumentNullException();

            var encoder = GetEncoder(input.FileExtension);

            if (encoder == null) throw new InvalidDataException();

            using var output = new MemoryStream();
            using Image<Rgba32> image = Image.Load<Rgba32>(input.Stream);

            if (width == 0) width = image.Width;

            if (maxWidth != 0 && maxWidth < width) width = maxWidth;

            decimal divisor = (decimal)image.Width / width;

            var imageHeight = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

            image.Mutate(x => x.Resize(width, imageHeight));
            image.Save(output, encoder);
            output.Position = 0;
            return new BasicFileStream(output.ToArray(), input.FileName);
        }

    }
}

