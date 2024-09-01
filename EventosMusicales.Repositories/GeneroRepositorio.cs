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
        public GeneroRepositorio(AplicactionDbContext db) : base(db)
        {

        }
    }
}
