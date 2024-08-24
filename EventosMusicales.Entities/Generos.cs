using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
    public class Generos
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Estado { get; set; } = true;
    }
}
