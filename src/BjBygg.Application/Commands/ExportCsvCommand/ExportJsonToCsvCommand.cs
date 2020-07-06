using BjBygg.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BjBygg.Application.Commands.ExportCsvCommands
{
    public class ExportJsonToCsvCommand : IRequest<Uri>
    {
        [Required]
        public Dictionary<string, string> PropertyMap { get; set; }

        [Required]
        public List<JsonElement> JsonObjects { get; set; }

        //public string Delimiter { get; set; } = ",";
    }
}
