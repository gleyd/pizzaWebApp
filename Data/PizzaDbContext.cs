using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPizza.Models;

namespace WebAppPizza.Data
{
    public class PizzaDbContext : DbContext   // un context signifie un environnement, un context de db
    {
        // DbSet s'occupe de faire le Mapping
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Command> Command { get; set; }
        public DbSet<DetailCommand> DetailCommand { get; set; }


        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {

        }
        public PizzaDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PizzaDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>(entity =>
            { 
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.PriceHT).IsRequired();
                entity.Property(e => e.Description).IsRequired();

                entity.HasMany(e => e.DetailCommands)
                      .WithOne(e => e.Pizza)
                      .HasForeignKey(e => e.IdPizza)
                      .IsRequired();
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.Property(e => e.CommandDate).IsRequired();

                entity.HasMany(e => e.DetailCommands)
                      .WithOne(e => e.Command)
                      .HasForeignKey(e => e.IdPizza)
                      .IsRequired();
            });
        }

    }
}
