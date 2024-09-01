using Azure;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/conciertos")]
    public class ConciertoController:ControllerBase
    {
        private readonly IConciertoRepository conciertorepository;
        private readonly IGeneroRepositorio generoRepositorio;
        private readonly ILogger<ConciertoController> logger;

        public ConciertoController(IConciertoRepository Conciertorepository, 
            IGeneroRepositorio generoRepositorio, ILogger<ConciertoController> logger)
        {
            conciertorepository = Conciertorepository;
            this.generoRepositorio = generoRepositorio;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<ConciertoResponseDto>>();
            try
            {
                //mapping
                var conciertosdb= await conciertorepository.GetAsync();
                var conciertosresponseDto=conciertosdb.Select(x=> new ConciertoResponseDto
                {
                    Id=x.Id,
                    Title= x.Title,
                    Description=x.Description,
                    Place=x.Place,
                    UnitPrice=x.UnitPrice,
                    GenreId=x.GenreId,
                    GeneroNombre= x.Genre.Name,
                    DateEvent =x.DateEvent,
                    ImageUrl=x.ImageUrl,
                    TicketsQuantity=x.TicketsQuantity,
                    Finalized=x.Finalized,
                    Estado=x.Estado
                }).ToList();
                response.data = conciertosresponseDto;
                response.Succes = true;
                logger.LogInformation("Obteniendo todos los conciertos");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la informacion";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
                return BadRequest(response);
            }

        }

        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var response = new BaseResponseGeneric<ICollection<ConciertoResponseDto>>();
            try
            {
                //mapping
                var conciertosdb = await conciertorepository.GetAsync(x=>x.Title.Contains(title ?? string.Empty),
                    x=>x.DateEvent); // Si es nulo el title 
                var conciertosresponseDto = conciertosdb.Select(x => new ConciertoResponseDto
                {
                    Title = x.Title,
                    Description = x.Description,
                    Place = x.Place,
                    UnitPrice = x.UnitPrice,
                    GenreId = x.GenreId,
                    DateEvent = x.DateEvent,
                    ImageUrl = x.ImageUrl,
                    TicketsQuantity = x.TicketsQuantity,
                    Finalized = x.Finalized
                }).ToList();
                response.data = conciertosresponseDto;
                response.Succes = true;
                logger.LogInformation($"Obteniendo todos los conciertos con titulo : {title}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la informacion";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
                return BadRequest(response);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Post(ConciertoRequestDto conciertoRequestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                //valida si existe el genero
                var genero = await generoRepositorio.GetAsync(conciertoRequestDto.GenreId);
                if(genero is null)
                {
                    response.ErrorMessage = $"El concierto no se pudo guardar, campo genero con id : {conciertoRequestDto.GenreId} no existe";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }

                //mapping
                var conciertodb = new Concierto()
                {
                    Title = conciertoRequestDto.Title,
                    Description = conciertoRequestDto.Description,
                    Place = conciertoRequestDto.Place,
                    UnitPrice = conciertoRequestDto.UnitPrice,
                    GenreId = conciertoRequestDto.GenreId,
                    DateEvent = conciertoRequestDto.DateEvent,
                    ImageUrl = conciertoRequestDto.ImageUrl,
                    TicketsQuantity = conciertoRequestDto.TicketsQuantity
                };
                response.data = await conciertorepository.AddAsync(conciertodb);
                response.Succes = true;
                logger.LogInformation($"Se guardo el concierto con el  id : {conciertodb.Id}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al guardar la informaciòn";
                logger.LogError($"{response.ErrorMessage} - {ex.Message} ");
                return BadRequest(response);
            }

        }



    }
}
