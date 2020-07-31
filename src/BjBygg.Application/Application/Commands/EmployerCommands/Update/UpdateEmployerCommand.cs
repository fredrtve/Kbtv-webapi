using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerCommand : UpdateCommand<EmployerDto>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class UpdateEmployerCommandHandler : UpdateCommandHandler<Employer, UpdateEmployerCommand, EmployerDto>
    {
        public UpdateEmployerCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
