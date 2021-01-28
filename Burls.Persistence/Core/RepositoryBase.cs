using Burls.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Core
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity == null)
            {
                return entity;
            }

            return await DeleteAsync(entity);
        }

        public virtual Task<TEntity> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return Task.FromResult(entity);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = _context.Set<TEntity>().Where(x => ids.Contains(x.Id));

            await DeleteRangeAsync(entities);
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);

            return Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            var entity = await _context.Set<TEntity>()
                .IncludeDefault()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var list = await _context.Set<TEntity>()
                .IncludeDefault()
                .ToListAsync();

            return list;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            return Task.FromResult(entity);
        }
    }
}
