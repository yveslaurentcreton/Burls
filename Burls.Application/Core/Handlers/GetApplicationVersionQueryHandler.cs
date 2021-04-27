using Burls.Application.Core.Queries;
using Burls.Application.Core.Responses;
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
    public class GetApplicationVersionQueryHandler : IRequestHandler<GetApplicationVersionQuery, GetApplicationVersionQueryResponse>
    {
        private readonly IApplicationService _applicationService;

        public GetApplicationVersionQueryHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Task<GetApplicationVersionQueryResponse> Handle(GetApplicationVersionQuery request, CancellationToken cancellationToken)
        {
            var version = _applicationService.GetVersion();

            return Task.FromResult(new GetApplicationVersionQueryResponse(version));
        }
    }
}
