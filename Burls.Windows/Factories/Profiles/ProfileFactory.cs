using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Profiles
{
    public class ProfileFactory
    {
        public virtual IReadOnlyList<InstalledProfile> GetProfiles()
        {
            return new List<InstalledProfile>() { InstalledProfile.Default() };
        }
    }
}
