using Burls.Core.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class Profile
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string IconPath { get; private set; }

        public ImageSource IconImageSource => IconPath != null ? Icon.ExtractAssociatedIcon(IconPath).ToImageSource() : null;

        public static Profile Default()
        {
            return new Profile() { Name = "Default" };
        }

        private Profile()
        {
        }

        public Profile(string name, string iconPath = null)
        {
            Name = name;
            IconPath = iconPath;
        }
    }
}
