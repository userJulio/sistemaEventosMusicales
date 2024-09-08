using EventosMusicales.Dto.Request;
using EventosMusicales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public interface IVentaRepository: IReposotoryBase<Venta>
    {
        Task CreateTransactionAsing();
        Task RollBackAsync();

        Task<ICollection<Venta>> GetAsync<Tkey>(Expression<Func<Venta,bool>> predicate,
                                                Expression<Func<Venta,Tkey>> orderby,
                                                PaginationDto paginationDto);
    }
}
