using AutoMapper;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using EventosMusicales.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Profiles
{
    public class ConciertoProfile: Profile
    {
        public ConciertoProfile()
        {
            //desde origen => destino
            CreateMap<ConciertoInfo, ConciertoResponseDto>()
                 .ForMember(d=>d.GeneroNombre, o=>o.MapFrom(x=>x.GeneroName));
            CreateMap<Concierto, ConciertoResponseDto>()
                  .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.DateEvent.ToShortDateString()))
                  .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DateEvent.ToShortTimeString()))
                  .ForMember(d => d.Estado, o => o.MapFrom(x => x.Estado ? "Activo" : "Inactivo"))
                  .ForMember(d => d.GeneroNombre, o => o.MapFrom(x=> x.Genre.Name ));
            
            CreateMap<ConciertoRequestDto, Concierto>()
                .ForMember(d=>d.DateEvent,o=>o.MapFrom(x=> Convert.ToDateTime($"{x.DateEvent} {x.TimeEvent}")));
        }
    }
}
