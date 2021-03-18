using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Create
{
    class CreateEmployerCommandProfile : Profile
    {
        public CreateEmployerCommandProfile()
        {
            CreateMap<CreateEmployerCommand, Employer>();
        }
    }
}
