using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationHeader<T>(this HttpContext httpcontext, IQueryable<T> queryable)
        {
            if(httpcontext is null)
                throw new ArgumentNullException(nameof(httpcontext));

            double totalRecords = await queryable.CountAsync();
            httpcontext.Response.Headers.Add("TotalRecordsQuantity",totalRecords.ToString());
        }
    }
}
