using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public interface IConciertoRepository: IReposotoryBase<Concierto>
    {
        Task<ICollection<ConciertoInfo>> GetAsync(string? title);

        Task<ICollection<ConciertoInfo>> getDataConcert(string? title,PaginationDto paginationDto);

        Task FinalizeAsync(int id);

    }
}
