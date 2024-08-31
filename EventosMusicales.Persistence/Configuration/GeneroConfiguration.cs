using EventosMusicales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Persistence.Configuration
{
    public class GeneroConfiguration : IEntityTypeConfiguration<Generos>
    {
        public void Configure(EntityTypeBuilder<Generos> builder)
        {
            //Todas las personalizaciones de la entidad se hace aqui
            builder.Property(x => x.Name).HasMaxLength(100);

            //Agrega la tabla generos al esquema musicales
            builder.ToTable(nameof(Generos), "Musicales");

        }
    }
}
