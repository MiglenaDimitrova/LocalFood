namespace LocalFood.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Common;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var emailAdmin = "admin@abv.bg";
            if (!dbContext.Users.Any(u => u.Email == emailAdmin))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@abv.bg",
                    UserName = "admin@abv.bg",
                    NormalizedEmail = "ADMIN@ABV.BG",
                    IsProducer = false,
                };
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "123456");
                user.PasswordHash = hashed;
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                await userManager.AddToRolesAsync(user, new[] { GlobalConstants.AdministratorRoleName });
            }
        }
    }
}