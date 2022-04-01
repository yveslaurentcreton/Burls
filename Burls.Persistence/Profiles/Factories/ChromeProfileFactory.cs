using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Factories
{
    public class ChromeProfileFactory : ChromiumProfileFactory
    {
        protected override string GetUserDataPath()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userData = Path.Combine(local, "Google", "Chrome", "User Data");

            return userData;
        }

        protected override InstalledProfile GetProfile(string name, string displayName)
        {
            var iconPath = Path.Combine(GetUserDataPath(), name, "Google Profile.ico");

            return new InstalledProfile(name, displayName, iconPath);
        }
    }
}
