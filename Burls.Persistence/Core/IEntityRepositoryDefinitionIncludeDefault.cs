using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Core
{
    public interface IEntityRepositoryDefinitionIncludeDefault<T>
    {
        IQueryable<T> IncludeDefault(IQueryable<T> query);
    }
}
