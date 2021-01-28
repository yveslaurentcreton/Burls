using Burls.Domain;
using Burls.Persistence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Data
{
    public class BrowserRepository : RepositoryBase<Browser, BurlsDbContext>, IBrowserRepository
    {
        public BrowserRepository(BurlsDbContext context)
            : base(context)
        {
        }
    }
}
