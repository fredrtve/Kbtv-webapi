using AutoMapper;
using BjBygg.Core.Entities;
namespace BjBygg.Application.Application.Commands.EmployerCommands.Update
{
    class UpdateEmployerProfile : Profile
    {
        public UpdateEmployerProfile()
        {
            CreateMap<UpdateEmployerCommand, Employer>();
        }
    }
}
