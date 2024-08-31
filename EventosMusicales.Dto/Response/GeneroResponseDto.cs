using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Response
{
    public class GeneroResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Estado { get; set; } = true;
    }
}
