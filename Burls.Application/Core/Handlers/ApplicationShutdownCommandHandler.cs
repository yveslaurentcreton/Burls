using Burls.Application.Core.Commands;
using Burls.Application.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Handlers
{
    public class ApplicationShutdownCommandHandler : IRequestHandler<ApplicationShutdownCommand>
    {
        private readonly IApplicationService _applicationService;

        public ApplicationShutdownCommandHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Task<Unit> Handle(ApplicationShutdownCommand request, CancellationToken cancellationToken)
        {
            _applicationService.Shutdown();

            return Unit.Task;
        }
    }
}
