using CleanArchitecture.Core;
using CleanArchitecture.Core.Interfaces.Services;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BjBygg.Application
{
    public class PdfReportMissionExtractor : IPdfReportMissionExtractor
    {
        public MissionPdfDto TryExtract(Stream pdf)
        {
            MissionPdfDto mission;
            try
            {
                var pdfReader = new PdfReader(pdf);
                var pdfDocument = new PdfDocument(pdfReader);
                var page = pdfDocument.GetFirstPage();

                var strategy = new SimpleTextExtractionStrategy();
                var rawString = PdfTextExtractor.GetTextFromPage(page, strategy);

                mission = GetMissionFromRawString(rawString);
                mission.Image = GetImageStreamFromFirstPage(page);

                pdfDocument.Close();
                pdfReader.Close();
            }
            catch
            {
                mission = null;
            }

            return mission;
        }

        private Stream GetImageStreamFromFirstPage(PdfPage page)
        {
            var dict = page.GetPdfObject();
            PdfDictionary pageResources = dict.GetAsDictionary(PdfName.Resources);

            var pageXObjects = pageResources.GetAsDictionary(PdfName.XObject);

            var keys = pageXObjects.KeySet().ToArray();
            PdfName imgName = keys.Last(); //Assume image is last on page 

            var stream = pageXObjects.GetAsStream(imgName);
            stream.SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);
            var imgObject = new PdfImageXObject(stream);
            var ext = imgObject.IdentifyImageFileExtension();

            //var image = new Image(imgObject);
            //image = ScaleImage(image, 400);
            //image.SetBackgroundColor(Color.WHITE);

            //var xo = image.GetXObject();
            //xo.SetModified();
            //var img = new PdfStream(image.GetXObject().GetPdfObject().GetBytes());

            //var strem = xo.GetPdfObject().GetBytes();

            return new MemoryStream(imgObject.GetImageBytes());
        }


        private MissionPdfDto GetMissionFromRawString(string rawString)
        {
            var mission = new MissionPdfDto();
            mission.Address = GetAddressFromRawString(rawString);
            mission.PhoneNumber = GetPhoneNumberFromRawString(rawString);
            return mission;
        }

        private string GetAddressFromRawString(string rawString)
        {
            var startKeyword = "Skadestedets";
            var additional = " addresse";
            var endKeyword = " \nKontaktperson";
            var startStringPosition = rawString.IndexOf(startKeyword) + startKeyword.Length + additional.Length;
            var endStringPosition = rawString.IndexOf(endKeyword);
            var address = rawString.Substring(startStringPosition,
                endStringPosition - startStringPosition);
            var lastSeperator = address.LastIndexOf(" \n");
            address = address.Remove(lastSeperator, 2).Insert(lastSeperator, ", ");
            address = address.Replace("\n", "");

            return address;
        }

        private string GetPhoneNumberFromRawString(string rawString)
        {
            var startKeyword = "Kontaktperson";
            var endKeyword = "\nSkadedato";

            var startStringPosition = rawString.IndexOf(startKeyword) + startKeyword.Length;

            var endStringPosition = rawString.IndexOf(endKeyword);

            var contactInfoString = rawString.Substring(startStringPosition,
                endStringPosition - startStringPosition);

            return Regex.Match(contactInfoString, @"\b\d{8,10}\b").Groups[0].Value;
        }

        private Image ScaleImage(Image image, int newWidth)
        {
            var width = image.GetImageWidth();
            var height = image.GetImageHeight();
            var scale = width / newWidth;
            return image.ScaleAbsolute(width, height * scale);
        }
    }
}
