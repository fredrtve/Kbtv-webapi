using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Identity.Queries.UserQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries
{
    public class ApplicationUserListQuery : IRequest<List<ApplicationUserDto>> { }

    public class ApplicationUserListHandler : IRequestHandler<ApplicationUserListQuery, List<ApplicationUserDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ApplicationUserListHandler(IAppDbContext dbContext, IMediator mediator, IMapper mapper)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<List<ApplicationUserDto>> Handle(ApplicationUserListQuery request, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<List<ApplicationUserDto>>(await _mediator.Send(new UserListQuery()));

            var employerUserDict = (await _dbContext.EmployerUsers.ToListAsync())
                .ToDictionary(x => x.UserName, x => x.EmployerId);

            users.ForEach(x =>
            {
                string employerId;
                employerUserDict.TryGetValue(x.UserName, out employerId);
                if (employerId != null) x.EmployerId = employerId;
            });

            return users;
        }
    }
}
