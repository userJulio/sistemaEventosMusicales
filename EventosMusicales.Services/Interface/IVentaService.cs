using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Interface
{
    public interface IVentaService
    {
        Task<BaseResponseGeneric<int>> AddVentaAsync(string email, VentaRequestDto requestDto);

        Task<BaseResponseGeneric<VentaResponseDto>> GetAsync(int id);

        Task<BaseResponseGeneric<ICollection<VentaResponseDto>>> Getasync(VentaByDateSearchDto search, PaginationDto paginationDto);
        Task<BaseResponseGeneric<ICollection<VentaResponseDto>>> Getasync(string email,string? title, PaginationDto paginationDto);

    }
}
