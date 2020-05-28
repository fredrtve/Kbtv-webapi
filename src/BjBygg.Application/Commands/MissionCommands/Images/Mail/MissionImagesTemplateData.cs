using CleanArchitecture.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BjBygg.Application.Commands.MissionCommands.Images.Mail
{

    public class MissionImagesTemplateData : ITemplateData
    {
        [JsonProperty("missions")]
        public IEnumerable<MissionImagesTemplateMissionDto> Missions { get; set; }
    }

    public class MissionImagesTemplateMissionDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("images")]
        public IEnumerable<string> Images { get; set; }
    }

}
