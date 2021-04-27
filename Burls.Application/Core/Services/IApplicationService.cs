using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface IApplicationService
    {
        IDictionary GetProperties();
        void AddProperty(object key, object value);
        void Shutdown();
        Version GetVersion();
    }
}
