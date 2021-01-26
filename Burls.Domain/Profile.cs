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
        public string Name { get; protected set; }
        public string IconPath { get; }

        public ImageSource IconImageSource => IconPath != null ? Icon.ExtractAssociatedIcon(IconPath).ToImageSource() : null;

        public static Profile Default()
        {
            return new Profile() { Name = "Default" };
        }

        protected Profile()
        {
        }

        public Profile(string name, string iconPath = null)
        {
            Name = name;
            IconPath = iconPath;
        }
    }
}
