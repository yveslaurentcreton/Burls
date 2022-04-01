using Burls.Domain;
using Newtonsoft.Json.Linq;
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
                .Select(x => {
                    var preferencesJson = File.ReadAllText(Path.Combine(x.Parent.FullName, "Preferences"));
                    dynamic preferences = JObject.Parse(preferencesJson);
                    string profileName = preferences.profile.name;
                    profileName = string.IsNullOrEmpty(profileName) ? x.Parent.Name : profileName;
                    return GetProfile(x.Parent.Name, profileName);
                }).ToList();
        }

        protected virtual InstalledProfile GetProfile(string name, string displayName)
        {
            return new InstalledProfile(name, displayName);
        }

        protected abstract string GetUserDataPath();
    }
}
