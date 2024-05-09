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
    }
}