using EventosMusicales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class GeneroRepositorio
    {
        private readonly List<Generos> listGeneros;
        public GeneroRepositorio()
        {
            listGeneros=new List<Generos>();
            listGeneros.Add(new Generos() { Id = 1, Name = "Salsa" });
            listGeneros.Add(new Generos() { Id = 2, Name = "Cumbia" });
            listGeneros.Add(new Generos() { Id = 3, Name = "Rock" });

        }

        public List<Generos> GetGeneros()
        {
            return listGeneros;
        }

        public Generos? GetGeneros(int id)
        {
            var item= listGeneros.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public Generos Add(Generos gener)
        {
            var lastItem = listGeneros.MaxBy(x => x.Id);
            gener.Id = lastItem is null ? 1: lastItem.Id +1;
            listGeneros.Add(gener);
            return gener;

        }
        public void Update(int id, Generos genre)
        {
            var item = GetGeneros(id);
            if(item is not null)
            {
                item.Name = genre.Name;
                item.Estado = genre.Estado;
            }
            
        }
        public void Delete(int id)
        {
            var item = GetGeneros(id);
            if(item is not null)
            {
                listGeneros.Remove(item);
            }
        }

    }
}
