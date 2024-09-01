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

            var response = new BaseResponseGeneric<ICollection<GeneroResponseDto>>();
            try
            {
                //MAPPING
                var generos = await repositorio.GetAsync();
                var generosResponseDto = generos.Select(x => new GeneroResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Estado = x.Estado
                }).ToList();

                response.data = generosResponseDto;
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
                var generos= await repositorio.GetAsync(idGenero);
               
                if (generos is null)
                {
                    logger.LogWarning($"El genero musical con el id {idGenero} no se encontro");
                    response.ErrorMessage = "No se encontrol el genero";
                    return NotFound(response);
                }
                else
                {
                    var generesponseDto = new GeneroResponseDto()
                    {
                        Id = generos.Id,
                        Name = generos.Name,
                        Estado = generos.Estado
                    };
                    response.data = generesponseDto;
                    response.Succes = true;
                    logger.LogInformation($"Obtener el genero musical con el id : {idGenero}");
                    

                }
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
                var genero = new Generos()
                {
                    Name=generorequestDTO.Name,
                    Estado=generorequestDTO.Estado
                };

               response.data= await repositorio.AddAsync(genero);
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
                var generoDb= await repositorio.GetAsync(idGenero);
                if(generoDb is null)
                {
                    response.ErrorMessage = "No se encontro el registro";
                    return NotFound(response);
                }
                generoDb.Name=genrequestDTO.Name;
                generoDb.Estado = genrequestDTO.Estado;

                await repositorio.UpdateAsync();
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
                var generoDb = await repositorio.GetAsync(idGenero);
                if (generoDb is null)
                {
                    response.ErrorMessage = "No se encontro el registro";
                    return NotFound(response);
                }

                await repositorio.DeleteAsync(idGenero);
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
