
using BjBygg.Application.Application.Common;
using BjBygg.Application.Common;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail
{

    public class MissionDocumentsTemplate : IMailTemplate<MissionDocumentsTemplateData>
    {
        public MissionDocumentsTemplate(IEnumerable<MissionDocument> documents, ResourceFolders resourceFolders, Stream attachment = null, string attachmentName = null)
        {
            Attachment = attachment;
            AttachmentName = attachmentName;
            Data = new MissionDocumentsTemplateData(documents.GroupBy(x => x.Mission).Select(x => new MissionDocumentsTemplateMission
            {
                Id = x.Key.Id,
                Address = x.Key.Address,
                Documents = x.Key.MissionDocuments.Select(x => new MissionDocumentsTemplateDocument()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = resourceFolders.GetUrl(resourceFolders.Document, x.FileName)
                }).ToList(),
            }).ToList());
        }

        public string Key => "d-2d52ec6ee61044f0a705e04ca87740d2";

        [JsonProperty("data")]
        public MissionDocumentsTemplateData Data { get; set; }

        public Stream Attachment { get; set; }

        public string AttachmentName { get; set; }
    }
    public class MissionDocumentsTemplateData
    {
        public MissionDocumentsTemplateData(List<MissionDocumentsTemplateMission> missions)
        {
            Missions = missions;
        }

        [JsonProperty("missions")]
        public List<MissionDocumentsTemplateMission> Missions { get; set; }
    }

    public class MissionDocumentsTemplateMission
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("documents")]
        public List<MissionDocumentsTemplateDocument> Documents { get; set; }
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
