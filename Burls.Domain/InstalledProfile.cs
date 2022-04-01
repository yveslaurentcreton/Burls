using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class InstalledProfile
    {
        public string Name { get; protected set; }
        public string DisplayName { get; protected set; }
        public string IconPath { get; protected set; }

        public static InstalledProfile Default()
        {
            return new InstalledProfile() { Name = "Default", DisplayName = "Default" };
        }

        private InstalledProfile()
        {
        }

        public InstalledProfile(string name, string displayName, string iconPath = null)
        {
            Name = name;
            DisplayName = displayName;
            IconPath = iconPath;
        }
    }
}
