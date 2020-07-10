using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommandHandler : IRequestHandler<UpdateTimesheetStatusCommand, TimesheetDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetStatusCommandHandler(IAppDbContext dbContext, IMapper mapper)
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
