using Burls.Application.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Queries
{
    public class GetApplicationVersionQuery : IRequest<GetApplicationVersionQueryResponse>
    {
    }
}
