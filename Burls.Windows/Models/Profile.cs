using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Windows.Models
{
    public class Profile
    {
        public string Name { get; protected set; }
        public ImageSource IconImageSource { get; }

        public static Profile Default()
        {
            return new Profile() { Name = "Default" };
        }

        protected Profile()
        {
        }

        public Profile(string name, ImageSource iconImageSource = null)
        {
            Name = name;
            IconImageSource = iconImageSource;
        }
    }
}
