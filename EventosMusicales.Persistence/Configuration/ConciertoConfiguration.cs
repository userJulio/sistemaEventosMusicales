using EventosMusicales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Persistence.Configuration
{
    public class ConciertoConfiguration : IEntityTypeConfiguration<Concierto>
    {
        public void Configure(EntityTypeBuilder<Concierto> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Place).HasMaxLength(100);
            //Setear el tipo que va tener en sql la propiedad DateEvent
            builder.Property(x => x.DateEvent)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");  //por defecto trae la fecha actual
            builder.Property(x=>x.ImageUrl).HasMaxLength(200)
                    .IsUnicode(false);
            builder.HasIndex(x => x.Title);  //Agrega un indice

            //Agrega la tabla concierto al esquema musicales
            builder.ToTable(nameof(Concierto), "Musicales");

        }
    }
}
