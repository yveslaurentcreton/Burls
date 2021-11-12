using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows
{
    public class MainViewModel
    {
        public string Name { get; set; }

        public MainViewModel(string name)
        {
            Name = name;
        }
    }
}
