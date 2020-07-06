using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerHandler : UpdateHandler<Employer, UpdateEmployerCommand, EmployerDto>
    {
        public UpdateEmployerHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
