using Burls.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Factories
{
    public abstract class ChromiumProfileFactory : ProfileFactory
    {
        public override IReadOnlyList<InstalledProfile> GetProfiles()
        {
            return (new DirectoryInfo(GetUserDataPath()))
                .EnumerateDirectories()
                .SelectMany(x => x.EnumerateDirectories("Storage"))
                .Select(x => GetProfile(x.Parent.Name))
                .ToList();
        }

        protected virtual InstalledProfile GetProfile(string name)
        {
            return new InstalledProfile(name);
        }

        protected abstract string GetUserDataPath();
    }
}
