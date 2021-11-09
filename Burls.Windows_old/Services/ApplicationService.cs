using Burls.Application.Browsers.Services;
using Burls.Application.Core.Services;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class ApplicationService : IApplicationService
    {
        public void AddProperty(object key, object value) => System.Windows.Application.Current.Properties.Add(key, value);

        public IDictionary GetProperties() => System.Windows.Application.Current.Properties;

        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public Version GetVersion()
        {
            // Set the app version in Burls.Windows > Properties > Package > PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version);
        }
    }
}
