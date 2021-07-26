namespace LocalFood.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LocalFood.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Плодове" });
            await dbContext.Categories.AddAsync(new Category { Name = "Зеленчуци" });
            await dbContext.Categories.AddAsync(new Category { Name = "Млечни" });
            await dbContext.Categories.AddAsync(new Category { Name = "Месни" });
            await dbContext.Categories.AddAsync(new Category { Name = "Яйца" });
            await dbContext.Categories.AddAsync(new Category { Name = "Ядки и сушени плодове" });
            await dbContext.Categories.AddAsync(new Category { Name = "Семена" });
            await dbContext.Categories.AddAsync(new Category { Name = "Разсад и посадъчен материал" });
            await dbContext.Categories.AddAsync(new Category { Name = "Мед и пчелни продукти" });
            await dbContext.Categories.AddAsync(new Category { Name = "Риба и морски дарове" });
            await dbContext.Categories.AddAsync(new Category { Name = "Зърнени" });
            await dbContext.Categories.AddAsync(new Category { Name = "Консервирана храна" });
            await dbContext.Categories.AddAsync(new Category { Name = "Други" });
        }
    }
}
