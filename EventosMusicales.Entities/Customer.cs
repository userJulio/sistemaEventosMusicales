﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Entities
{
    public class Customer:EntityBase
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; }=default!;

    }
}
