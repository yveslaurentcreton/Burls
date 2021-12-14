using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Data
{
    public interface ISettingsRepository
    {
        Settings GetSettings();
        void SaveSettings(Settings settings);
    }
}