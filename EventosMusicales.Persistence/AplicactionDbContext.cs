using EventosMusicales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Persistence
{
    public class AplicactionDbContext : DbContext
    {
        public AplicactionDbContext(DbContextOptions options) : base(options)
        {

        }

        //Fluent API= Serie de pasos en el que mi modelo se va transformar en objetos de la bd

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            //Definiir la migracion de clase a tabla : Code First Approach
            // modelbuilder.Entity<Generos>().Property(x => x.Name).HasMaxLength(100);
            modelbuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //Entidades a Tablas
       // public DbSet<Generos> GenerosMusicales { get; set; }


    }
}
