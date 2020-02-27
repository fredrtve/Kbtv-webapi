using BjBygg.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdResponse
    {
        public MissionDto Mission { get; set; }
        public List<MissionImageDto> MissionImages { get; set; }
        public List<MissionReportDto> MissionReports { get; set; }
        public List<MissionDetailNoteDto> MissionNotes { get; set; }

    }


}
