
using BjBygg.Application.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail
{

    public class MissionDocumentsTemplate : IMailTemplate<IEnumerable<MissionDocumentsTemplateDocument>>
    {
        public MissionDocumentsTemplate(IEnumerable<MissionDocument> documents)
        {
            Data = documents.Select(x => new MissionDocumentsTemplateDocument()
               {
                   Id = x.Id,
                   DocumentTypeName = x.DocumentType == null ? "Ukategorisert" : x.DocumentType.Name,
                   Url = new StorageFileUrl(x.FileName, ResourceFolderConstants.Document).FileUrl
               }).ToList();
        }

        public string Key => "d-2d52ec6ee61044f0a705e04ca87740d2";

        [JsonProperty("data")]
        public IEnumerable<MissionDocumentsTemplateDocument> Data { get; set; }
    }

    public class MissionDocumentsTemplateDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("documentTypeName")]
        public string DocumentTypeName { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }


}
