
using BjBygg.SharedKernel;
using MediatR;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommand : IRequest
    {
        public CreateMissionWithPdfCommand() { }
        public CreateMissionWithPdfCommand(DisposableList<BasicFileStream> files)
        {
            Files = files;
        }

        //[JsonIgnore]
        public DisposableList<BasicFileStream> Files { get; set; }

    }
}
