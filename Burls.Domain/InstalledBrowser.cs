using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class InstalledBrowser
    {
        public string Name { get; protected set; }
        public string ExecutablePath { get; protected set; }
        public string IconPath { get; protected set; }
        public int IconIndex { get; protected set; }
        public string ProfileArgumentName { get; protected set; }
        public virtual IReadOnlyList<InstalledProfile> Profiles { get; protected set; }

        private InstalledBrowser() { }

        public InstalledBrowser(
            string name,
            string executablePath,
            string iconPath,
            int iconIndex,
            string profileArgumentName,
            IReadOnlyList<InstalledProfile> profiles)
        {
            Name = name;
            ExecutablePath = executablePath;
            IconPath = iconPath;
            IconIndex = iconIndex;
            ProfileArgumentName = profileArgumentName;
            Profiles = profiles;
        }
    }
}
