using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Responses
{
    public class GetAllPropertiesQueryResponse
    {
        public IDictionary<string, object> Properties { get; }

        public GetAllPropertiesQueryResponse(IDictionary<string, object> properties)
        {
            Properties = properties;
        }
    }
}
