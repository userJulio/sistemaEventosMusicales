using AutoMapper;
using EventosMusicales.Dto.Request;
using EventosMusicales.Dto.Response;
using EventosMusicales.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Services.Profiles
{
    public class VentaProfile:Profile
    {

        private static readonly CultureInfo culture = new("es-PE");

        public VentaProfile()
        {
            //origen => destino
            CreateMap<VentaRequestDto, Venta>()
                .ForMember(d => d.Quantity, o => o.MapFrom(x => x.TikectQuantity))
                .ForMember(d => d.ConcertId, o => o.MapFrom(x => x.ConciertoId));

            CreateMap<Venta, VentaResponseDto>()
                .ForMember(d => d.VentaId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.concierto.DateEvent.ToString("D", culture)))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.concierto.DateEvent.ToString("T", culture)))
                .ForMember(d => d.Genero, o => o.MapFrom(x => x.concierto.Genre.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.concierto.ImageUrl))
                .ForMember(d => d.Title, o => o.MapFrom(x => x.concierto.Title))
                .ForMember(d => d.FullName, o => o.MapFrom(x => x.customer.FullName))
                .ForMember(d => d.DateVenta, o => o.MapFrom(x => x.FechaVenta.ToString("dd/MM/yyyy HH:mm", culture)));
        }
    }
}
