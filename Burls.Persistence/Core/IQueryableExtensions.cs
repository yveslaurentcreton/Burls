using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Core
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> IncludeDefault<T>(this IQueryable<T> query)
        {
            var type = typeof(IEntityRepositoryDefinitionIncludeDefault<T>);
            var implementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => type.IsAssignableFrom(t));
            var implementation = implementations.Single();
            var instance = Activator.CreateInstance(implementation) as IEntityRepositoryDefinitionIncludeDefault<T>;

            return instance.IncludeDefault(query);
        }
    }
}
