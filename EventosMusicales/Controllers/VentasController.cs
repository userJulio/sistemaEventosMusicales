using EventosMusicales.Dto.Request;
using EventosMusicales.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EventosMusicales.Controllers
{
    [ApiController]
    [Route("api/Ventas")]
    public class VentasController:ControllerBase
    {
        private readonly IVentaService ventaService;

        public VentasController(IVentaService ventaService)
        {
            this.ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await ventaService.GetAsync(id);
            return response.Succes? Ok(response): BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaRequestDto ventaRequestDto)
        {
            var response = await ventaService.AddVentaAsync(ventaRequestDto.Email, ventaRequestDto);
            return response.Succes ? Ok(response) : BadRequest(response);

        }
        [HttpGet("ListVentaByDate")]
        public async Task<IActionResult> GetByDate([FromQuery] VentaByDateSearchDto ventaByDateSearch,
                                                        [FromQuery]PaginationDto paginationDto )
        {
            var response = await ventaService.Getasync(ventaByDateSearch,paginationDto);
 
            return response.Succes ? Ok(response) : BadRequest( response);
        }

        [HttpGet("ListaVentas")]
        public async Task<IActionResult> GetVentas(string email, [FromQuery]string? title , 
                                                        [FromQuery] PaginationDto paginationDto)
        {
            var response = await ventaService.Getasync(email, title, paginationDto);

            return response.Succes ? Ok(response) : BadRequest(response);
        }


    }
}
