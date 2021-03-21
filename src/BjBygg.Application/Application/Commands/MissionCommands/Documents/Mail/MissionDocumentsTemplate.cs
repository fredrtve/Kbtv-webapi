
using BjBygg.Application.Application.Common;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail
{

    public class MissionDocumentsTemplate : IMailTemplate<MissionDocumentsTemplateData>
    {
        public MissionDocumentsTemplate(IEnumerable<MissionDocument> documents, BasicFileStream attachment = null)
        {
            Attachment = attachment;
            Data = new MissionDocumentsTemplateData(documents.GroupBy(x => x.Mission).Select(x => new MissionDocumentsTemplateMission
            {
                Id = x.Key.Id,
                Address = x.Key.Address,
                Documents = x.Key.MissionDocuments.Select(x => new MissionDocumentsTemplateDocument()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = new StorageFileUrl(x.FileName, ResourceFolderConstants.Document).FileUrl.ToString()
                }).ToList(),
            }).ToList());
        }

        public string Key => "d-2d52ec6ee61044f0a705e04ca87740d2";

        [JsonProperty("data")]
        public MissionDocumentsTemplateData Data { get; set; }

        public BasicFileStream Attachment { get; set; }
    }
    public class MissionDocumentsTemplateData
    {
        public MissionDocumentsTemplateData(IEnumerable<MissionDocumentsTemplateMission> missions)
        {
            Missions = missions;
        }

        [JsonProperty("missions")]
        public IEnumerable<MissionDocumentsTemplateMission> Missions { get; set; }
    }

    public class MissionDocumentsTemplateMission
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("documents")]
        public IEnumerable<MissionDocumentsTemplateDocument> Documents { get; set; }
    }

    public class MissionDocumentsTemplateDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
