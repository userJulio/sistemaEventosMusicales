using EventosMusicales.Entities;
using System.Linq.Expressions;

namespace EventosMusicales.Repositories
{
    public interface IReposotoryBase<TEntity> where TEntity : EntityBase
    {
        Task<int> AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<ICollection<TEntity>> GetAsync();
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetAsync(int id);
        Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, Tkey>> orderby);
        Task UpdateAsync();
    }
}