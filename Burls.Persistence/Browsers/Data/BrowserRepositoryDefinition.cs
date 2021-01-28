using Burls.Domain;
using Burls.Persistence.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Data
{
    public class BrowserRepositoryDefinition : IEntityRepositoryDefinitionIncludeDefault<Browser>
    {
        public IQueryable<Browser> IncludeDefault(IQueryable<Browser> query)
        {
            return query.Include(x => x.Profiles)
                .ThenInclude(x => x.Websites);
        }
    }
}
