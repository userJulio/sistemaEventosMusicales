	using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
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

        public async Task<List<GeneroResponseDto>> GetGeneros()
        {

            var items = await context.GenerosMusicales.ToListAsync();
            //List<GeneroResponseDto> listaresponse = new List<GeneroResponseDto>();
            //foreach (var item in items) {
            //    listaresponse.Add(new GeneroResponseDto() {
            //        Id=item.Id ,
            //        Name=item.Name,
            //        Estado=item.Estado 
            //    });          
            //}

            var listadto = items.Select(x => new GeneroResponseDto { 
                Id = x.Id, 
                Name = x.Name,
                Estado = x.Estado }).ToList();

            return listadto;
        }

        public async Task<GeneroResponseDto?> GetGeneros(int id)
        {
            // El ? puede devolver un objeto genero o un nulo.
            //var item = listGeneros.FirstOrDefault(x => x.Id == id);
            //return item;


            var item = await context.GenerosMusicales.FirstOrDefaultAsync(x => x.Id == id);
            GeneroResponseDto generodto = new GeneroResponseDto();
            if (item is not null)
            {
                //mapping

                generodto.Id = item.Id;
                generodto.Name = item.Name;
                generodto.Estado = item.Estado;
            }
            else
            {
                throw new InvalidOperationException($"No se encontro el registro con id : {id}");
            }
            return generodto;
        }
        public async Task<int> Add(GeneroRequestDto geneRequestDto)
        {
            //var lastItem = listGeneros.MaxBy(x => x.Id);
            //gener.Id = lastItem is null ? 1 : lastItem.Id + 1;
            //listGeneros.Add(gener);
            //return gener;
            var generoMusical = new Generos()
            {
                Name = geneRequestDto.Name,
                Estado = geneRequestDto.Estado
            };


            context.GenerosMusicales.Add(generoMusical);
            await context.SaveChangesAsync();
            return generoMusical.Id;
        }
        public async Task Update(int id, GeneroRequestDto generRequestDto)
        {
            var item = await context.GenerosMusicales.FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                item.Name = generRequestDto.Name;
                item.Estado = generRequestDto.Estado;
                context.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se actualizo el genero musical");
            }


        }
        public async Task Delete(int id)
        {
            var item = await context.GenerosMusicales.FirstOrDefaultAsync(x => x.Id == id);
            if (item is not null)
            {


                context.GenerosMusicales.Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se elimino el género musical ");
            }

        }

    }
}
