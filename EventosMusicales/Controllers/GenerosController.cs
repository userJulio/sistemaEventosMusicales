using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroRepositorio repositorio;
        private readonly ILogger<GenerosController> logger;

        public GenerosController(IGeneroRepositorio repositorio,ILogger<GenerosController> logger)
        {
            this.repositorio = repositorio;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var response = new BaseResponseGeneric<IEnumerable<GeneroResponseDto>>();
            try
            {
                response.data= await repositorio.GetGeneros();
                response.Succes = true;
                logger.LogInformation("Obteniendo todos los generos musicales");
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurriio un error al obtener la informacion de Generos";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }

        }


        [HttpGet("{idGenero:int}")]
        public async Task<IActionResult> GetId(int idGenero)
        {
            var response =new BaseResponseGeneric<GeneroResponseDto>();

            try
            {
                response.data= await repositorio.GetGeneros(idGenero);
                response.Succes = true;
                logger.LogInformation($"Obtener el genero musical con el id : {idGenero}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error para obtener la informacion de generos";
                logger.LogError($"{response.ErrorMessage} {ex.Message} ");
                return BadRequest(response);
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(GeneroRequestDto generorequestDTO)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
               response.data= await repositorio.Add(generorequestDTO);
                response.Succes = true;
                logger.LogInformation($"Genero musical insertado correctamente con el id : {response.data}");
                //return Ok(response);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un problema al agregar un nuevo registro en Generos";
                logger.LogError($"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);
               
            }
        }

        [HttpPut("{idGenero:int}")]
        public async Task<IActionResult> Put(int idGenero, GeneroRequestDto genrequestDTO)
        {
            var response = new BaseResponse();
            try
            {
                await repositorio.Update(idGenero, genrequestDTO);
                response.Succes = true;
                logger.LogInformation($"Se actualizo correctamente el genero musical con id : {idGenero}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Ocurrio un error al actualizar el genero musical con id : {idGenero}";
                logger.LogError($"{response.ErrorMessage} -  {ex.Message}");
                return BadRequest(response);
                
            }
        }

        [HttpDelete("{idGenero:int}")]
        public async Task<IActionResult> Delete(int idGenero)
        {
            var response = new BaseResponse();
            try
            {
                await repositorio.Delete(idGenero);
                response.Succes = true;
                logger.LogInformation($"Se elimino correctamente el genero musical con id : {idGenero}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Hubo un problema al eliminar el genero musical con id : {idGenero}";
                logger.LogError($" {response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);

            }
        }


    }
}
