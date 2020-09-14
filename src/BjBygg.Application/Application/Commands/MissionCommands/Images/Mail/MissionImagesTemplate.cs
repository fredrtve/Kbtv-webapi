using BjBygg.Application.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Mail
{

    public class MissionImagesTemplate : IMailTemplate<IEnumerable<MissionImagesTemplateMission>>
    {
        public MissionImagesTemplate(IEnumerable<MissionImage> images)
        {
            Data = images.GroupBy(x => x.Mission).Select(x => new MissionImagesTemplateMission
            {
                Id = x.Key.Id,
                Address = x.Key.Address,
                Images = x.Select(x => 
                    new StorageFileUrl(x.FileName, ResourceFolderConstants.Image).FileUrl
                ).ToList(),
            }).ToList();
        }

        public string Key => "d-d6066dc9b4cc495aa8b2b9c1e0dd8afc";

        [JsonProperty("data")]
        public IEnumerable<MissionImagesTemplateMission> Data { get; set; }
    }

    public class MissionImagesTemplateMission
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("images")]
        public IEnumerable<Uri> Images { get; set; }
    }

}
