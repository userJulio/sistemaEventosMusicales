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
    public class GeneroRepositorio : IGeneroRepositorio
    {
        //private readonly List<Generos> listGeneros;
        private readonly AplicactionDbContext context;

        public GeneroRepositorio(AplicactionDbContext context)
        {
            //listGeneros = new List<Generos>();
            //listGeneros.Add(new Generos() { Id = 1, Name = "Salsa" });
            //listGeneros.Add(new Generos() { Id = 2, Name = "Cumbia" });
            //listGeneros.Add(new Generos() { Id = 3, Name = "Rock" });
            this.context = context;
        }

        public async Task<List<Generos>> GetGeneros()
        {
            return await context.GenerosMusicales.ToListAsync();
        }

        public async Task<Generos?> GetGeneros(int id)
        {
            // El ? puede devolver un objeto genero o un nulo.
            //var item = listGeneros.FirstOrDefault(x => x.Id == id);
            //return item;
            var item = await context.GenerosMusicales.FirstOrDefaultAsync(x => x.Id == id);
            if(item is not null)
            {
                return item;
            }
            else
            {
                throw new InvalidOperationException($"No se encontro el registro con id : {id}");
            }
        }
        public async Task<int> Add(Generos gener)
        {
            //var lastItem = listGeneros.MaxBy(x => x.Id);
            //gener.Id = lastItem is null ? 1 : lastItem.Id + 1;
            //listGeneros.Add(gener);
            //return gener;
            context.GenerosMusicales.Add(gener);
            await context.SaveChangesAsync();
            return gener.Id;
        }
        public async Task Update(int id, Generos genre)
        {
            //var item = GetGeneros(id);
            //if (item is not null)
            //{
            //    item.Name = genre.Name;
            //    item.Estado = genre.Estado;
            //}else
            //{

            //}
            var item = await GetGeneros(id);
            if (item is not null)
            {
                item.Name = genre.Name;
                item.Estado = genre.Estado;
                context.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se actualizo el genero musical con id :{id}");
            }


        }
        public async Task Delete(int id)
        {
            //var item = GetGeneros(id);
            //if (item is not null)
            //{
            //    listGeneros.Remove(item);
            //}
            var item = await GetGeneros(id);
            if (item is not null)
            {
                context.GenerosMusicales.Remove(item);
                await context.SaveChangesAsync();
            }
            else{
                throw new InvalidOperationException($"No se elimino el género musical con id :{id}");
            }

        }

    }
}
