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
        void Shutdown();
        ApplicationTheme GetTheme();
        void SetTheme(ApplicationTheme theme);
        Version GetVersion();
    }

    public enum ApplicationTheme
    {
        OsDefault,
        Light,
        Dark
    }
}
