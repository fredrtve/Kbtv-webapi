
using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
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
