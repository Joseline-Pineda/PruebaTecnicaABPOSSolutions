using Microsoft.AspNetCore.Identity;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Data.Migrations
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            context.Database.EnsureCreated();

            if (context.Users.Any()) return;
            CreateUser(userManager, "admin@example.com", "Admin", "User", true);
            CreateUser(userManager, "user@example.com", "Regular", "User", false);
        }

        private static void CreateUser(UserManager<User> userManager, 
            string email, 
            string nombres, 
            string apellidos, 
            bool isAdmin)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                Nombres = nombres,
                Apellidos = apellidos,
                IsAdmin = isAdmin,
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(user, "P@ssw0rd").Result;

            if (!result.Succeeded)
            {
                throw new Exception($"Error al crear el usuario '{email}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
