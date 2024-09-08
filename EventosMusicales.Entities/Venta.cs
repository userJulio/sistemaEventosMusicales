using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
    public class Venta:EntityBase
    {
        
        public int CustomerId { get; set; }

        public int ConcertId { get; set; }

        public DateTime FechaVenta { get; set; }
        public string NumeroOperacion { get; set; } = default!;

        public decimal Total { get; set; }
        public short Quantity { get; set; }

        //propiedades de navegacion
        [ForeignKey("CustomerId")]
        public virtual Customer customer { get; set; } = default!;

        [ForeignKey("ConcertId")]
        public virtual Concierto concierto { get; set; } = default!;

    }
}
