using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    [DebuggerDisplay("{Name}")]
    [Serializable]
    public class Browser : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string IconPath { get; set; }
        public int IconIndex { get; set; }
        public string ProfileArgumentName { get; set; }
        public ICollection<Profile> Profiles { get; set; }

        protected Browser() { }

        public string GetProfileArgument(Profile profile)
        {
            var profileArgument = string.Empty;

            if (!string.IsNullOrEmpty(ProfileArgumentName))
            {
                profileArgument = $"{ProfileArgumentName}=\"{profile.Name}\"";
            }

            return profileArgument;
        }

        public void NavigateToUrl(string url, Profile profile)
        {
            var profileArgument = GetProfileArgument(profile);
            var urlArgument = $"\"{url}\"";
            var argumentList = new List<string>() { profileArgument, urlArgument };
            var arguments = string.Join(' ', argumentList);

            Process.Start(ExecutablePath, arguments);
        }
    }
}
