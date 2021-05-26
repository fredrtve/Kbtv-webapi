using BjBygg.Application.Common.Interfaces;
using System.IO;

namespace BjBygg.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommand : IOptimisticCommand
    {
        public string Id { get; set; }
        public string FileExtension { get; set; }
        public Stream Image { get; set; }
    }
}
