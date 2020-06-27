using Burls.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class ProfileService : IProfileService
    {
        public IReadOnlyList<Profile> GetProfiles(string browserFileName)
        {
            var profiles = new List<Profile>();

            switch (browserFileName)
            {
                case Browser.CHROMEFILENAME:
                    profiles.AddRange(GetChromeProfiles());
                    break;
                case Browser.NEWEDGEFILENAME:
                    profiles.AddRange(GetNewEdgeProfiles());
                    break;
                default:
                    profiles.Add(Profile.Default());
                    break;
            }

            return profiles
                .OrderBy(x => x.Name)
                .ToList();
        }

        public IReadOnlyList<Profile> GetChromeProfiles()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var chromeUserData = Path.Combine(local, "Google", "Chrome", "User Data");

            return GetChromiumProfiles(chromeUserData);
        }

        public IReadOnlyList<Profile> GetNewEdgeProfiles()
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var chromeUserData = Path.Combine(local, "Microsoft", "Edge", "User Data");

            return GetChromiumProfiles(chromeUserData);
        }

        private IReadOnlyList<Profile> GetChromiumProfiles(string chromiumUserDataFilename)
        {
            return (new DirectoryInfo(chromiumUserDataFilename))
                .EnumerateDirectories()
                .SelectMany(x => x.EnumerateDirectories("Storage"))
                .Select(x => new Profile(x.Parent.Name))
                .ToList();
        }
    }
}
