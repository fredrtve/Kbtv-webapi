using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommandHandler : IRequestHandler<UpdateTimesheetStatusCommand, TimesheetDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetStatusCommandHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TimesheetDto> Handle(UpdateTimesheetStatusCommand request, CancellationToken cancellationToken)
        {
            var dbTimesheet = await _dbContext.Set<Timesheet>().FindAsync(request.Id);

            if (dbTimesheet == null)
                throw new EntityNotFoundException(nameof(Timesheet), request.Id);

            dbTimesheet.Status = request.Status;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TimesheetDto>(dbTimesheet);
        }
    }
}
