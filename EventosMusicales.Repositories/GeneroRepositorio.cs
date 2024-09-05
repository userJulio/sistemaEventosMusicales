	using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
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
    public class GeneroRepositorio : ReposotoryBase<Generos>, IGeneroRepositorio
    {
        public GeneroRepositorio(AplicactionDbContext context) : base(context)
        {

        }

        public async Task<ICollection<Generos>> GetGeneros(string? title)
        {
            var listaGeneros = await context.Set<Generos>().
                Where(x => x.Name.Contains(title ?? string.Empty))
                .AsNoTracking()
                .ToListAsync();

            return listaGeneros;

        }
    }
}
