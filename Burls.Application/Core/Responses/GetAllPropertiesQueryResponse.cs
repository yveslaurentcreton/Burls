using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Responses
{
    public class GetAllPropertiesQueryResponse
    {
        public IDictionary Properties { get; }

        public GetAllPropertiesQueryResponse(IDictionary properties)
        {
            Properties = properties;
        }
    }
}
