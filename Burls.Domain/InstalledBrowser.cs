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
        public string Name { get; private set; }
        public string ExecutablePath { get; private set; }
        public string IconPath { get; private set; }
        public int IconIndex { get; private set; }
        public string ProfileArgumentName { get; private set; }
        public virtual IReadOnlyList<InstalledProfile> Profiles { get; private set; }

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
