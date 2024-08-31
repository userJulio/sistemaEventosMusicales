using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
   public class Concierto:EntityBase
    {
       
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public double UnitPrice { get; set; }
        public int GenreId { get; set; }
        public DateTime DateEvent { get; set; }
        public string? ImageUrl { get; set; }
        public int TicketsQuantity { get; set; }
        public bool Finalized { get; set; }
        //Navigation properties : Propiedades de Navegacion para hacer Foreingkey
     
        public Generos Genre { get; set; } = default!;
    }
}
