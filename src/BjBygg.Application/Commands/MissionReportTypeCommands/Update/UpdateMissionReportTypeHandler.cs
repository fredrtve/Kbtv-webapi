using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.ReportTypeCommands.Update
{
    public class UpdateReportTypeHandler : IRequestHandler<UpdateReportTypeCommand, ReportTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateReportTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ReportTypeDto> Handle(UpdateReportTypeCommand request, CancellationToken cancellationToken)
        {
            var dbReportType = await _dbContext.ReportTypes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbReportType == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id") continue;
                dbReportType.GetType().GetProperty(property.Name).SetValue(dbReportType, property.GetValue(request), null);
            }


            try { await _dbContext.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong when storing your request");
            }

            return _mapper.Map<ReportTypeDto>(dbReportType);
        }
    }
}
