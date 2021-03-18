using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BjBygg.Infrastructure.Services
{
    public class PdfMissionExtractionOneStrategy : IPdfMissionExtractionStrategy
    {
        public MissionPdfDto TryExtract(Stream pdf)
        {
            MissionPdfDto missionPdf;
            try
            {
                var pdfReader = new PdfReader(pdf);
                var pdfDocument = new PdfDocument(pdfReader);
                var page = pdfDocument.GetFirstPage();

                var strategy = new SimpleTextExtractionStrategy();
                var rawString = PdfTextExtractor.GetTextFromPage(page, strategy);

                missionPdf = GetMissionPdfFromRawString(rawString);
                missionPdf.Image = GetImageStreamFromFirstPage(page);

                pdfDocument.Close();
                pdfReader.Close();
            }
            catch
            {
                missionPdf = null;
            }

            return missionPdf;
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


        private MissionPdfDto GetMissionPdfFromRawString(string rawString)
        {
            var missionPdf = new MissionPdfDto();
            missionPdf.Address = GetAddressFromRawString(rawString);
            missionPdf.PhoneNumber = GetPhoneNumberFromRawString(rawString);
            missionPdf.DocumentName = GetDocumentNameFromRawString(rawString);
            return missionPdf;
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
        private string GetDocumentNameFromRawString(string rawString)
        {
            var endKeyword = "\nInformasjon om skaden";

            var endStringPosition = rawString.IndexOf(endKeyword);

            var startStringPosition = rawString.Substring(0, endStringPosition).LastIndexOf("\n") + 2;

            var name = rawString.Substring(startStringPosition,
                endStringPosition - startStringPosition);

            return Regex.Replace(name, @"\s+", ""); //Remove whitespaces
        }


    }
}
