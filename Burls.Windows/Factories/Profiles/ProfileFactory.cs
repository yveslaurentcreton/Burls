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
        public virtual IReadOnlyList<Profile> GetProfiles()
        {
            return new List<Profile>() { Profile.Default() };
        }
    }
}
