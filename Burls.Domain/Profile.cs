using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class Profile : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string IconPath { get; private set; }

        public ICollection<Website> Websites { get; set; }

        private Profile()
        {
        }
    }
}
