using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Domain
{
    public class Website : IEntity
    {
        public int Id { get; private set; }
        public string Domain { get; private set; }

        public Website(string domain)
        {
            Domain = domain;
        }
    }
}
