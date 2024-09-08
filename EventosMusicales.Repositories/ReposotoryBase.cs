using EventosMusicales.Entities;
using EventosMusicales.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public abstract class ReposotoryBase<TEntity> :  IReposotoryBase<TEntity> where TEntity : EntityBase
    {

        protected readonly DbContext context;

        protected ReposotoryBase(DbContext context)
        {
            this.context = context;
        }

        public virtual async Task<ICollection<TEntity>> GetAsync()
        {
            //AsNoTracking hace mas rapido el get
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predicate,
                                                                Expression<Func<TEntity, Tkey>> orderby)
        {
            return await context.Set<TEntity>().
                Where(predicate).
                OrderBy(orderby).
                AsNoTracking().
                ToListAsync();
        }
        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
        public virtual async Task UpdateAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);
            if (item is not null)
            {
                item.Estado = false;
                await UpdateAsync();
            }
        }

    }
}
