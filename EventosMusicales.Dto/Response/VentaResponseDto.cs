using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Response
{
    public class VentaResponseDto
    {
        public int VentaId { get; set; }
        public string DateEvent { get; set; } = default!;
        public string  TimeEvent { get; set; }= default!;
        public string Genero { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public string Title { get; set; } = default!;
        public string NumeroOperacion { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public short Quantity { get; set; }
        public string DateVenta { get; set; } = default!;
        public decimal Total { get; set; }
    }
}
