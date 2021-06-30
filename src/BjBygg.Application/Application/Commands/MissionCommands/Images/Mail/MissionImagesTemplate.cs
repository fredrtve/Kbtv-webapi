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

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Mail
{

    public class MissionImagesTemplate : IMailTemplate<MissionImagesTemplateData>
    {
        public MissionImagesTemplate(IEnumerable<MissionImage> images, ResourceFolders resourceFolders, Stream attachment, string attachmentName)
        {
            Attachment = attachment;
            AttachmentName = attachmentName;
            Data = new MissionImagesTemplateData(images.GroupBy(x => x.Mission).Select(x => new MissionImagesTemplateMission
            {
                Id = x.Key.Id,
                Address = x.Key.Address,
                Images = x.Select(x => resourceFolders.GetUrl(resourceFolders.Document, x.FileName)).ToList(),
            }).ToList());
        }

        public string Key => "d-d6066dc9b4cc495aa8b2b9c1e0dd8afc";

        [JsonProperty("data")]
        public MissionImagesTemplateData Data { get; set; }

        public Stream Attachment { get; set; }

        public string AttachmentName { get; set; }
    }

    public class MissionImagesTemplateData
    {
        public MissionImagesTemplateData(IEnumerable<MissionImagesTemplateMission> missions)
        {
            Missions = missions;
        }

        [JsonProperty("missions")]
        public IEnumerable<MissionImagesTemplateMission> Missions { get; set; }
    }

    public class MissionImagesTemplateMission
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("images")]
        public IEnumerable<string> Images { get; set; }
    }

}
