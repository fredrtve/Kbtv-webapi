using BjBygg.Application.Application.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public class MissionSyncArraysResponse
    {
        public DbSyncArrayResponse<MissionDto> Missions { get; set; }

        public DbSyncArrayResponse<MissionImageDto> MissionImages { get; set; }

        public DbSyncArrayResponse<MissionNoteDto> MissionNotes { get; set; }

        public DbSyncArrayResponse<MissionDocumentDto> MissionDocuments { get; set; }

    }
}
