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
    public class AddPropertyCommandHandler : IRequestHandler<AddPropertyCommand>
    {
        private readonly IApplicationService _applicationService;

        public AddPropertyCommandHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Task<Unit> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            _applicationService.AddProperty(request.Key, request.Value);

            return Unit.Task;
        }
    }
}
