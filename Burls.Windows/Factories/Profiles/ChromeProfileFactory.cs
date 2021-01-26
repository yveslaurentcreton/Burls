using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Profiles
{
    public class ChromeProfileFactory : ChromiumProfileFactory
    {
        protected override string GetUserDataPath()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userData = Path.Combine(local, "Google", "Chrome", "User Data");

            return userData;
        }

        protected override Profile GetProfile(string name)
        {
            var iconPath = Path.Combine(GetUserDataPath(), name, "Google Profile.ico");

            return new Profile(
                name,
                iconPath);
        }
    }
}
