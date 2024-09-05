using Azure;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using EventosMusicales.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/conciertos")]
    public class ConciertoController:ControllerBase
    {
        private readonly IConciertoService serviceConcierto;

        public ConciertoController(IConciertoService serviceConcierto)
        {
            this.serviceConcierto = serviceConcierto;
        }

        [HttpGet("SearchByTitle")]
        public async Task<IActionResult> GetResponse(string? title)
        {
            var response = await serviceConcierto.GetAsync(title);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpGet("titulo/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await serviceConcierto.GetAsync(id);
            return response.Succes ? Ok(response) : BadRequest(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ConciertoRequestDto conciertoRequestDto)
        {
            var response= await serviceConcierto.Addasync(conciertoRequestDto);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, ConciertoRequestDto conciertoRequestDto)
        {
            var response = await serviceConcierto.Update(id, conciertoRequestDto);
            return response.Succes? Ok(response): BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await serviceConcierto.DeleteAsync(id);
            return response.Succes?Ok(response): BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> ActualizarFinalized(int id)
        {
            var response= await serviceConcierto.FinalizeAsync(id);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

      



        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await conciertorepository.GetAsync());

        //    var response = new BaseResponseGeneric<ICollection<ConciertoResponseDto>>();
        //    try
        //    {
        //        //mapping
        //        var conciertosdb = await conciertorepository.GetAsync();
        //        var conciertosresponseDto = conciertosdb.Select(x => new ConciertoResponseDto
        //        {
        //            Id = x.Id,
        //            Title = x.Title,
        //            Description = x.Description,
        //            Place = x.Place,
        //            UnitPrice = x.UnitPrice,
        //            GenreId = x.GenreId,
        //            GeneroNombre = x.Genre.Name,
        //            DateEvent = x.DateEvent,
        //            ImageUrl = x.ImageUrl,
        //            TicketsQuantity = x.TicketsQuantity,
        //            Finalized = x.Finalized,
        //            Estado = x.Estado
        //        }).ToList();
        //        response.data = conciertosresponseDto;
        //        response.Succes = true;
        //        logger.LogInformation("Obteniendo todos los conciertos");
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al obtener la informacion";
        //        logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
        //        return BadRequest(response);
        //    }

        //}
        //[HttpGet("ReporteInfo")]
        ////Este metodo trae toda la informacion del Concierto info
        //public async Task<IActionResult> GetReporte(string? title)
        //{
        //    var conciertos=await conciertorepository.GetAsync(title);
        //    return Ok(conciertos);

        //}


       



    }
}
