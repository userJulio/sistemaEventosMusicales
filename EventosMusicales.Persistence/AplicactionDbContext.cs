using EventosMusicales.Entities;
using EventosMusicales.Entities.Info;
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
            modelbuilder.Entity<ConciertoInfo>().HasNoKey();  //Para que no se cree como tabla en sql
        }
        //Entidades a Tablas
        // public DbSet<Generos> GenerosMusicales { get; set; }

        //configuracion para usar lazi loading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();  //habilita los proxies para utilizar lazi loading
            }
        }


    }
}
