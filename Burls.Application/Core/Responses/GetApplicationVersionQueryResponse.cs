using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Responses
{
    public class GetApplicationVersionQueryResponse
    {
        public Version Version { get; }

        public GetApplicationVersionQueryResponse(Version version)
        {
            Version = version;
        }
    }
}
