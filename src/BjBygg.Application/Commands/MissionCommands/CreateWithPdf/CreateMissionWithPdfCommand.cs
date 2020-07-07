
using BjBygg.Application.Common;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommand : IRequest<MissionDto>
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
