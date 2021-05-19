using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BjBygg.Infrastructure.Services
{
    public class PdfMissionExtractionTwoStrategy : IPdfMissionExtractionStrategy
    {
        private static string AddressPattern = @"(?<=Skadested\nAdresse: )(.*)(?=\nKommune)"; 
        private static string PhoneNumberPattern = @"(?<=Mobiltlf:)(.*)(?=\nKonklusjon skadeårsak)";

        public MissionPdfDto TryExtract(Stream pdf)
        {
            MissionPdfDto missionPdf;

            var pdfReader = new PdfReader(pdf);
            var pdfDocument = new PdfDocument(pdfReader);
            string rawString;
            try
            {            
                var page = pdfDocument.GetFirstPage();

                var strategy = new SimpleTextExtractionStrategy();
                rawString = PdfTextExtractor.GetTextFromPage(page, strategy);
                missionPdf = GetMissionPdfFromRawString(rawString);
                missionPdf.Image = GetImageStreamFromFirstPage(page);        
            }
            catch(Exception ex)
            {
                missionPdf = null;
            }

            pdfDocument.Close();
            pdfReader.Close();

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

            return new MemoryStream(imgObject.GetImageBytes());
        }


        private MissionPdfDto GetMissionPdfFromRawString(string rawString)
        {
            var missionPdf = new MissionPdfDto();
            missionPdf.Address = GetAddressFromRawString(rawString);
            missionPdf.PhoneNumber = GetPhoneNumberFromRawString(rawString);
            missionPdf.DocumentName = "Skaderapport";
            return missionPdf;
        }

        private string GetAddressFromRawString(string rawString)
        {
            var match = Regex.Match(rawString, PdfMissionExtractionTwoStrategy.AddressPattern, RegexOptions.ECMAScript);
            return match.Groups.Values.FirstOrDefault()?.Value;
        }

        private string GetPhoneNumberFromRawString(string rawString)
        {
            var match = Regex.Match(rawString, PdfMissionExtractionTwoStrategy.PhoneNumberPattern, RegexOptions.ECMAScript);
            var number = match.Groups.Values.FirstOrDefault();
            return Regex.Replace(number?.Value, @"\s+", "");
        }


    }
}
