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
    }
}
