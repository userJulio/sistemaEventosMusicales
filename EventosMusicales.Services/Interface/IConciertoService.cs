using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Interface
{
    public interface IConciertoService
    {
        Task<BaseResponseGeneric<ICollection<ConciertoResponseDto>>> GetAsync(string? title);
        Task<BaseResponseGeneric<ConciertoResponseDto>> GetAsync(int id);

        Task<BaseResponseGeneric<int>> Addasync(ConciertoRequestDto requestDto);

        Task<BaseResponse> Update(int id, ConciertoRequestDto requestDto);
        Task<BaseResponse> DeleteAsync(int id);

        Task<BaseResponse> FinalizeAsync(int id);
    }
}
