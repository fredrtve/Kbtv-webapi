using AutoMapper;
using CleanArchitecture.Core.Entities;
namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    class UpdateEmployerCommandProfile : Profile
    {
        public UpdateEmployerCommandProfile()
        {
            CreateMap<UpdateEmployerCommand, Employer>();
        }
    }
}
