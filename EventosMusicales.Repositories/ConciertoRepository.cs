using EventosMusicales.Entities;
using EventosMusicales.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class ConciertoRepository : ReposotoryBase<Concierto>, IConciertoRepository
    {
        public ConciertoRepository(AplicactionDbContext context) : base(context)
        {

        }

        public override async Task<ICollection<Concierto>> GetAsync()
        {
            //eager loading approach : carga ansiosa
            return await context.Set<Concierto>().
                            Include(x => x.Genre).
                            AsNoTracking().
                            ToListAsync();
        }


    }
}
