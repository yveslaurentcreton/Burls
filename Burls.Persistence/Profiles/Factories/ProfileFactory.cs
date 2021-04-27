using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Factories
{
    public class ProfileFactory
    {
        public virtual IReadOnlyList<InstalledProfile> GetProfiles()
        {
            return new List<InstalledProfile>() { InstalledProfile.Default() };
        }
    }
}
