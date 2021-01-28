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
        public string Name { get; private set; }
        public string IconPath { get; private set; }

        public static InstalledProfile Default()
        {
            return new InstalledProfile() { Name = "Default" };
        }

        private InstalledProfile()
        {
        }

        public InstalledProfile(string name, string iconPath = null)
        {
            Name = name;
            IconPath = iconPath;
        }
    }
}
