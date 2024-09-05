using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities.Info
{
    public class ConciertoInfo
    {
        public int Id { get; set; }
   
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public Decimal UnitPrice { get; set; }
        public int GenreId { get; set; }
        public string GeneroName { get; set; } = default!;
        public string DateEvent { get; set; } = default!;
        public string TimeEvent { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public int TicketsQuantity { get; set; }
        public bool Finalized { get; set; }

        public string Estado { get; set; } = default!;
    }
}
