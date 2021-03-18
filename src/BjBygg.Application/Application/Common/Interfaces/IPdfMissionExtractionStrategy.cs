using BjBygg.Application.Application.Common.Dto;
using System.IO;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface IPdfMissionExtractionStrategy
    {
        MissionPdfDto TryExtract(Stream pdf);
    }
}
