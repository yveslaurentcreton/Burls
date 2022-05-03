using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    [DebuggerDisplay("{Name}")]
    [Serializable]
    public class Profile : IEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string IconPath { get; set; }

        public IList<SelectionRule> SelectionRules { get; set; }

        protected Profile()
        {
            SelectionRules = new List<SelectionRule>();
        }
    }
}
