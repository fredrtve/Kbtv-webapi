
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    [ApplicationExceptionFilter]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(IMediator mediator, ILogger<BaseController> logger) : base()
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected async Task<TResponse> ValidateAndExecute<TResponse>(IRequest<TResponse> request) 
        {
            if (!TryValidateModel(request))
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(request);
        }
    }

    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case BadRequestException e:
                    context.Result = new BadRequestObjectResult(e.Message);
                    return;
                case ConflictException e:
                    context.Result = new ConflictObjectResult(e.Message);
                    return;
                case EntityNotFoundException e:
                    context.Result = new NotFoundObjectResult(e.Message);
                    return;
                case ForbiddenException e:
                    context.Result = new ForbidResult();
                    return;
                case UnauthorizedException e:
                    context.Result = new UnauthorizedResult();
                    return;
            }
        }

    }
}