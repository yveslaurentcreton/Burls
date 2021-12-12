using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface IApplicationService
    {
        IDictionary<string, object> GetProperties();
        void AddProperty(object key, object value);
        void Shutdown();
        Version GetVersion();
        ApplicationTheme GetTheme();
        void SetTheme(ApplicationTheme theme);
    }

    public enum ApplicationTheme
    {
        OsDefault,
        Light,
        Dark
    }
}
