using EventosMusicales.Entities;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;

namespace EventosMusicales.Repositories
{
    public interface IGeneroRepositorio: IReposotoryBase<Generos>
    {
        //Task<int> Add(GeneroRequestDto gener);
        //Task Delete(int id);
        //Task<List<GeneroResponseDto>> GetGeneros();
        //Task<GeneroResponseDto?> GetGeneros(int id);
        //Task Update(int id, GeneroRequestDto genero);
        Task<ICollection<Generos>> GetGeneros(string? title);
    }
}