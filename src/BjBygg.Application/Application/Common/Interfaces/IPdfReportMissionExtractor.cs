using BjBygg.Application.Application.Common.Dto;
using System.IO;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IPdfReportMissionExtractor
    {
        MissionPdfDto TryExtract(Stream pdf);
    }
}
