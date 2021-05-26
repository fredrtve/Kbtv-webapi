
using BjBygg.SharedKernel;
using MediatR;
using System.IO;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommand : IRequest
    {
        public CreateMissionWithPdfCommand() { }
        public CreateMissionWithPdfCommand(DisposableList<Stream> files)
        {
            Files = files;
        }

        //[JsonIgnore]
        public DisposableList<Stream> Files { get; set; }

    }
}
