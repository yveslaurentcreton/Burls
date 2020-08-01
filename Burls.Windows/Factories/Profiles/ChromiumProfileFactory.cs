using Burls.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Profiles
{
    public abstract class ChromiumProfileFactory : ProfileFactory
    {
        public override IReadOnlyList<Profile> GetProfiles()
        {
            return (new DirectoryInfo(GetUserDataPath()))
                .EnumerateDirectories()
                .SelectMany(x => x.EnumerateDirectories("Storage"))
                .Select(x => GetProfile(x.Parent.Name))
                .ToList();
        }

        protected virtual Profile GetProfile(string name)
        {
            return new Profile(name);
        }

        protected abstract string GetUserDataPath();
    }
}
