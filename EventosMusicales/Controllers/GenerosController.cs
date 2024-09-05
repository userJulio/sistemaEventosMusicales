using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using EventosMusicales.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService generoService;

        public GenerosController(IGeneroService generoService)
        {
            this.generoService = generoService;
        }

        [HttpGet("GetbyName")]
        public async Task<IActionResult> Get(string? name)
        {
            var response= await generoService.GetAsync(name);
           return response.Succes? Ok(response): BadRequest(response);
        }


        [HttpGet("{idGenero:int}")]
        public async Task<IActionResult> GetId(int idGenero)
        {
            var response = await generoService.GetAsync(idGenero);
            return response.Succes? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(GeneroRequestDto generorequestDTO)
        {
            var response = await generoService.Addasync(generorequestDTO);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{idGenero:int}")]
        public async Task<IActionResult> Put(int idGenero, GeneroRequestDto genrequestDTO)
        {
            var response = await generoService.Update(idGenero, genrequestDTO);
            return response.Succes? Ok(response) : BadRequest(response);             
        }


        [HttpDelete("{idGenero:int}")]
        public async Task<IActionResult> Delete(int idGenero)
        {
            var response = await generoService.DeleteAsync(idGenero);
            return response.Succes? Ok(response) : BadRequest(response);
        }


    }
}
