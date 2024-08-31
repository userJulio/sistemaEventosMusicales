using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Request
{
    public class GeneroRequestDto
    {
        public string Name { get; set; } = default!;
        public bool Estado { get; set; } = true;
    }
}
