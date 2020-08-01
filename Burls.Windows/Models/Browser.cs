using Burls.Windows.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Windows.Models
{
    public class Browser
    {
        public string Name { get; }
        public string ExecutablePath { get; }
        public string IconPath { get; }
        public int IconIndex { get; }
        public ImageSource IconImageSource { get; }
        public FileVersionInfo Version { get; }
        public ReadOnlyDictionary<string, object> FileAssociations { get; }
        public ReadOnlyDictionary<string, object> UrlAssociations { get; }
        public string ProfileArgumentName { get; }
        public IReadOnlyList<Profile> Profiles { get; }

        public Browser(
            string name,
            string executablePath,
            string iconPath,
            int iconIndex,
            FileVersionInfo version,
            ReadOnlyDictionary<string, object> fileAssociations,
            ReadOnlyDictionary<string, object> urlAssociations,
            string profileArgumentName,
            IReadOnlyList<Profile> profiles)
        {
            Name = name;
            ExecutablePath = executablePath;
            IconPath = iconPath;
            IconIndex = iconIndex;
            Version = version;
            FileAssociations = fileAssociations;
            UrlAssociations = urlAssociations;
            IconImageSource = Icon.ExtractAssociatedIcon(IconPath).ToImageSource();
            ProfileArgumentName = profileArgumentName;
            Profiles = profiles;
        }

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
