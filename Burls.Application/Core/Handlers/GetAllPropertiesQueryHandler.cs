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
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, GetAllPropertiesQueryResponse>
    {
        private readonly IApplicationService _applicationService;

        public GetAllPropertiesQueryHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Task<GetAllPropertiesQueryResponse> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = _applicationService.GetProperties();

            return Task.FromResult(new GetAllPropertiesQueryResponse(properties));
        }
    }
}
