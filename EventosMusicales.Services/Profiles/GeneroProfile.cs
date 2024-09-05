using AutoMapper;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Profiles
{
   public class GeneroProfile:Profile
    {
        public GeneroProfile()
        {
            //origen : destion
            CreateMap<Generos,GeneroResponseDto>();
            CreateMap<GeneroRequestDto, Generos>();

        }

    }
}
