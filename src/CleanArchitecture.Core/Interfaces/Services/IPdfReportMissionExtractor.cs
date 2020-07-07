using System.IO;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IPdfReportMissionExtractor
    {
        MissionPdfDto TryExtract(Stream pdf);
    }
}
