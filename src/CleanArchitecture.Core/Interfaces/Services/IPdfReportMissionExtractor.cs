using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IPdfReportMissionExtractor
    {
        MissionPdfDto Extract(Stream pdf);
    }
}
