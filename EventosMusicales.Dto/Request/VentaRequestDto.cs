using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Request
{
    public record VentaRequestDto(int ConciertoId,short TikectQuantity,string Email, string FullName);
   
}
