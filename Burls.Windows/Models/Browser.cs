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

        public Browser(
            string name,
            string executablePath,
            string iconPath,
            int iconIndex,
            FileVersionInfo version,
            ReadOnlyDictionary<string, object> fileAssociations,
            ReadOnlyDictionary<string, object> urlAssociations)
        {
            Name = name;
            ExecutablePath = executablePath;
            IconPath = iconPath;
            IconIndex = iconIndex;
            Version = version;
            FileAssociations = fileAssociations;
            UrlAssociations = urlAssociations;
            IconImageSource = Icon.ExtractAssociatedIcon(IconPath).ToImageSource();
        }
    }
}
