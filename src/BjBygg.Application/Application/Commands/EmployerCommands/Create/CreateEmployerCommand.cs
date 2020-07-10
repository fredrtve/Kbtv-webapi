using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommand : CreateCommand<EmployerDto>
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
    public class CreateEmployerCommandHandler : CreateCommandHandler<Employer, CreateEmployerCommand, EmployerDto>
    {
        public CreateEmployerCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
