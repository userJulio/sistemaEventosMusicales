using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly GeneroRepositorio repositorio;

        public GenerosController(GeneroRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public ActionResult<List<Generos>> Get()
        {
            var data = repositorio.GetGeneros();
            return Ok(data);
        }


        [HttpGet("{idGenero:int}")]
        public ActionResult<Generos> GetId(int idGenero)
        {
            var genero = repositorio.GetGeneros(idGenero);
            return genero is not null ? Ok(genero) : NotFound();
        }

        [HttpPost]
        public ActionResult<Generos> Agregar(Generos genero)
        {
            repositorio.Add(genero);
            return Ok(genero);
        }
        [HttpPut("{idGenero:int}")]
        public ActionResult Put(int idGenero, Generos genre)
        {
            repositorio.Update(idGenero,genre);
            return Ok();
        }

        [HttpDelete("{idGenero:int}")]
        public ActionResult Delete(int idGenero)
        {
            repositorio.Delete(idGenero);
            return Ok();
        }


    }
}
