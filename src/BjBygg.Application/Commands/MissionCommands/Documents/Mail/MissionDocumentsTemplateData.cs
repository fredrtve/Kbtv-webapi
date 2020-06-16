using CleanArchitecture.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BjBygg.Application.Commands.MissionCommands.Documents.Mail
{

    public class MissionDocumentsTemplateData : ITemplateData
    {
        [JsonProperty("documents")]
        public IEnumerable<MissionDocumentsTemplateDocumentDto> Documents { get; set; }
    }

    public class MissionDocumentsTemplateDocumentDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("documentTypeName")]
        public string DocumentTypeName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
