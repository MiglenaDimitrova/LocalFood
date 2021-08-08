namespace LocalFood.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager);
    }
}
