using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Response
{
    public class ConciertoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public Decimal UnitPrice { get; set; }
        public int GenreId { get; set; }
        public string GeneroNombre { get; set; } = default!;
        public DateTime DateEvent { get; set; }
        public string? ImageUrl { get; set; }
        public int TicketsQuantity { get; set; }
        public bool Finalized { get; set; }
        public bool Estado { get; set; }

    }
}
