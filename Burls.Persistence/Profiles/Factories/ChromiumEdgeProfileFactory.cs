using Burls.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Factories
{
    public class ChromiumEdgeProfileFactory : ChromiumProfileFactory
    {
        protected override string GetUserDataPath()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userData = Path.Combine(local, "Microsoft", "Edge", "User Data");

            return userData;
        }

        protected override InstalledProfile GetProfile(string name, string displayName)
        {
            var iconPath = Path.Combine(GetUserDataPath(), name, "Edge Profile.ico");

            if (!File.Exists(iconPath))
            {
                iconPath = null;
            }

            return new InstalledProfile(name, displayName, iconPath);
        }
    }
}
