﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
    public class Generos:EntityBase
    {
        
        public string Name { get; set; } = default!;
    }
}
