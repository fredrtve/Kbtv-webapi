using CleanArchitecture.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BjBygg.Application.Commands.MissionCommands.Reports.Mail
{

    public class MissionReportsTemplateData : ITemplateData
    {
        [JsonProperty("missions")]
        public IEnumerable<MissionReportsTemplateReportDto> Reports { get; set; }
    }

    public class MissionReportsTemplateReportDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("reportTypeName")]
        public string ReportTypeName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
