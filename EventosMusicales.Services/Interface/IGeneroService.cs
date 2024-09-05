using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Interface
{
    public interface IGeneroService
    {
        Task<BaseResponseGeneric<ICollection<GeneroResponseDto>>> GetAsync(string? title);
        Task<BaseResponseGeneric<GeneroResponseDto>> GetAsync(int id);

        Task<BaseResponseGeneric<int>> Addasync(GeneroRequestDto requestDto);

        Task<BaseResponse> Update(int id, GeneroRequestDto requestDto);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
