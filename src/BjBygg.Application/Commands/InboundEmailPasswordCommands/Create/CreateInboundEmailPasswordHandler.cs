using AutoMapper;
using BjBygg.Application.Shared;
using BjBygg.Application.Shared.Dto;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordHandler : IRequestHandler<CreateInboundEmailPasswordCommand, InboundEmailPasswordDto>
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateInboundEmailPasswordHandler(AppIdentityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<InboundEmailPasswordDto> Handle(CreateInboundEmailPasswordCommand request, CancellationToken cancellationToken)
        {
            var password = new InboundEmailPassword() { Password = request.Password };

            _dbContext.Set<InboundEmailPassword>().Add(password);

            await _dbContext.SaveChangesAsync();

            return new InboundEmailPasswordDto() { Id = password.Id, Password = password.Password };
        }
    }
}
