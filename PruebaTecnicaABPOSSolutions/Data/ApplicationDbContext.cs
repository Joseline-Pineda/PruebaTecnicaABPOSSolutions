using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Negocio>? Negocios { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Menu>? Menus { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Entradas" },
                new Categoria { Id = 2, Nombre = "Platos Principales" },
                new Categoria { Id = 3, Nombre = "Postres" },
                new Categoria { Id = 4, Nombre = "Bebidas" },
                new Categoria { Id = 5, Nombre = "Especiales del Chef" }
            );
        }

    }
}