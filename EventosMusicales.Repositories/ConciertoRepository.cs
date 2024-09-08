using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Entities.Info;
using EventosMusicales.Persistence;
using EventosMusicales.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class ConciertoRepository : ReposotoryBase<Concierto>, IConciertoRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public ConciertoRepository(AplicactionDbContext context,IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }
        //Reportes ConciertoInfo

        public async Task<ICollection<ConciertoInfo>> GetAsync(string? title)
        {
            //eager loading optimizado
            //return await context.Set<Concierto>().Include(x => x.Genre)
            //            .Where(x => x.Title.Contains(title ?? string.Empty))
            //            .AsNoTracking()
            //            .Select(x => new ConciertoInfo()
            //            {
            //                Id = x.Id,
            //                Title = x.Title,
            //                Place = x.Place,
            //                UnitPrice = x.UnitPrice,
            //                GenreId = x.GenreId,
            //                GeneroName = x.Genre.Name,
            //                DateEvent = x.DateEvent.ToShortDateString(),
            //                TimeEvent = x.DateEvent.ToShortTimeString(),
            //                ImageUrl = x.ImageUrl,
            //                Finalized = x.Finalized,
            //                Estado = x.Estado ? "Activo" : "Inactivo"
            //            }).ToListAsync();

            //LAZI LOADING  : La unica diferencia con el anterior es que no tiene el Include
            //return await context.Set<Concierto>()
            //            .Where(x => x.Title.Contains(title ?? string.Empty))
            //            .AsNoTracking()
            //            .Select(x => new ConciertoInfo()
            //            {
            //                Id = x.Id,
            //                Title = x.Title,
            //                Place = x.Place,
            //                UnitPrice = x.UnitPrice,
            //                GenreId = x.GenreId,
            //                GeneroName = x.Genre.Name,
            //                DateEvent = x.DateEvent.ToShortDateString(),
            //                TimeEvent = x.DateEvent.ToShortTimeString(),
            //                ImageUrl = x.ImageUrl,
            //                Finalized = x.Finalized,
            //                Estado = x.Estado ? "Activo" : "Inactivo"
            //            }).ToListAsync();

            //RAW QUERY: Store Procedure
            //var query = context.Set<ConciertoInfo>()
            //                .FromSqlRaw("usp_ConcertsByTitle {0}", title?? string.Empty);
            //return await query.ToListAsync();
            var query = context.Set<ConciertoInfo>()
                            .FromSqlRaw("usp_ConcertsByTitle {0}", title ?? string.Empty);
            return await query.ToListAsync();

        }

        public async Task<ICollection<ConciertoInfo>> getDataConcert(string? title,PaginationDto paginationDto)
        {
            //IgnoreQueryFilters() es para que no ignore el query global que tiene el genero
            var queryableConcert=   context.Set<Concierto>()
                        .Where(x => x.Title.Contains(title ?? string.Empty))
                        .IgnoreQueryFilters()
                        .AsNoTracking()
                        .Select(x => new ConciertoInfo()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Place = x.Place,
                            UnitPrice = x.UnitPrice,
                            GenreId = x.GenreId,
                            GeneroName = x.Genre.Name,
                            DateEvent = x.DateEvent.ToShortDateString(),
                            TimeEvent = x.DateEvent.ToShortTimeString(),
                            ImageUrl = x.ImageUrl,
                            Finalized = x.Finalized,
                            Estado = x.Estado ? "Activo" : "Inactivo"
                        }).AsQueryable();

            await httpContext.HttpContext.InsertPaginationHeader(queryableConcert);
            return await queryableConcert.OrderBy(x=>x.Id).Paginate(paginationDto).ToListAsync();

            //var query = context.Set<Concierto>()
            //               .FromSqlRaw("usp_ConcertsByTitle {0}", title ?? string.Empty);
            //return await query.ToListAsync();

        }

        //public override async Task<ICollection<Concierto>> GetAsync()
        //{
        //    //eager loading approach : carga ansiosa
        //    return await context.Set<Concierto>().
        //                    Include(x => x.Genre).
        //                    AsNoTracking().
        //                    ToListAsync();
        //}

        public async Task FinalizeAsync(int id)
        {
            var item= await GetAsync(id);
            if(item is not null)
            {
                item.Finalized = true;
                await UpdateAsync();
            }

        }


    }
}
