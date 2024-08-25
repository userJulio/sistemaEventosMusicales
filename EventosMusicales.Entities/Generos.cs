using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
    public class Generos
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Estado { get; set; } = true;
    }
}
