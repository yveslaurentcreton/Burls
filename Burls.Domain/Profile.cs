using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class Profile : IEntity
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string IconPath { get; protected set; }

        public IList<SelectionRule> SelectionRules { get; set; }

        protected Profile()
        {
        }
    }
}
