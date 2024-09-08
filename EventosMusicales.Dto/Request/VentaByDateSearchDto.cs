using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Dto.Request
{
    public class VentaByDateSearchDto
    {
        public string DateStart { get; set; } = default!;
        public string DateEnd { get; set; } = default!;
    }
}
