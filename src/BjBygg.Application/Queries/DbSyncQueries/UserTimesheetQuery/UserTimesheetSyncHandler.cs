using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery
{
    public class UserTimesheetSyncHandler : UserDbSyncHandler<Timesheet, UserTimesheetSyncQuery, TimesheetDto>
    {
        public UserTimesheetSyncHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

    }
}
