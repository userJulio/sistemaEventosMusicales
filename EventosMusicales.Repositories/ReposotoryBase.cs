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
    public class ReposotoryBase<TEntity> :  IReposotoryBase<TEntity> where TEntity : EntityBase
    {

        private readonly DbContext db;

        public ReposotoryBase(DbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<TEntity>> GetAsync()
        {
            //AsNoTracking hace mas rapido el get
            return await db.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await db.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predicate,
                                                                Expression<Func<TEntity, Tkey>> orderby)
        {
            return await db.Set<TEntity>().
                Where(predicate).
                OrderBy(orderby).
                AsNoTracking().
                ToListAsync();
        }
        public async Task<TEntity?> GetAsync(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity.Id;
        }
        public async Task UpdateAsync()
        {
            await db.SaveChangesAsync();
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
