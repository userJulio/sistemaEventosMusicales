using EventosMusicales.Dto.Request;
using EventosMusicales.Entities;
using EventosMusicales.Persistence;
using EventosMusicales.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class VentaRespository : ReposotoryBase<Venta>, IVentaRepository
    {
        private readonly IHttpContextAccessor httpContext;

        //private readonly AplicactionDbContext context;

        public VentaRespository(AplicactionDbContext context,IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
            //  this.context = context;
        }

        public async Task CreateTransactionAsing()
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            
        }

        public async  Task RollBackAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }
        public override async Task<int> AddAsync(Venta entity)
        {
            entity.FechaVenta = DateTime.Now;
            var nextNumber = await context.Set<Venta>().CountAsync() + 1;
            entity.NumeroOperacion = $"{nextNumber:000000}";
            await context.AddAsync(entity);
            return entity.Id;
        }
        public override async Task<Venta?> GetAsync(int id)
        {
            return await context.Set<Venta>()
                .Include(x => x.customer)
                .Include(x => x.concierto).ThenInclude(x => x.Genre)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }
        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public async Task<ICollection<Venta>> GetAsync<Tkey>(Expression<Func<Venta, bool>> predicate,
            Expression<Func<Venta, Tkey>> orderby, PaginationDto paginationDto)
        {
            var queryable = context.Set<Venta>()
                            .Include(x => x.customer)
                            .Include(x => x.concierto)
                            .ThenInclude(x => x.Genre)
                            .Where(predicate)
                            .OrderBy(orderby)
                            .AsNoTracking()
                            .AsQueryable();
            // Este metodo inserta la cantidad de registros que tiene el queryable en el header HTTP
            await httpContext.HttpContext.InsertPaginationHeader(queryable);
            var response = await queryable.Paginate(paginationDto).ToListAsync();
            return response;
        }
    }
}
