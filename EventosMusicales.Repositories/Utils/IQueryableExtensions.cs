using EventosMusicales.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto paginationDto)
        {
            return queryable.Skip((paginationDto.Page - 1) * paginationDto.RecordPerPage)
                        .Take(paginationDto.RecordPerPage);

        }
    }
}
