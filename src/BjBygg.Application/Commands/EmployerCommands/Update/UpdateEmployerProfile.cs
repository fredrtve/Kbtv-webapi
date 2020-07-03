using AutoMapper;
using CleanArchitecture.Core.Entities;
namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    class UpdateEmployerProfile : Profile
    {
        public UpdateEmployerProfile()
        {
            CreateMap<UpdateEmployerCommand, Employer>();
        }
    }
}
