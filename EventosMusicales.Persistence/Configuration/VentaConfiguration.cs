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
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.Property(x=>x.NumeroOperacion).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.FechaVenta).HasColumnType("date").HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Total).HasColumnType("decimal(10,2)");
            builder.ToTable(nameof(Venta), "Musicales");

        }
    }
}
