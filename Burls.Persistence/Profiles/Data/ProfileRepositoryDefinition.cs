using Burls.Domain;
using Burls.Persistence.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Data
{
    public class ProfileRepositoryDefinition : IEntityRepositoryDefinitionIncludeDefault<Profile>
    {
        public IQueryable<Profile> IncludeDefault(IQueryable<Profile> query)
        {
            return query.Include(x => x.Websites);
        }
    }
}
