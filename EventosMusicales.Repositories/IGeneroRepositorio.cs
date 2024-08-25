using EventosMusicales.Entities;

namespace EventosMusicales.Repositories
{
    public interface IGeneroRepositorio
    {
        Task<int> Add(Generos gener);
        Task Delete(int id);
        Task<List<Generos>> GetGeneros();
        Task<Generos?> GetGeneros(int id);
        Task Update(int id, Generos genre);
    }
}