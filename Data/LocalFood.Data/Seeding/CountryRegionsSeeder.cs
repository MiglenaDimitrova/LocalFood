namespace LocalFood.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LocalFood.Data.Models;

    public class CountryRegionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            // Bulgaria
            var countryBulgaria = new Country { Name = "България" };
            await dbContext.Countries.AddAsync(countryBulgaria);
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Благоевград" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Бургас" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Варна" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Велико Търново" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Видин" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Враца" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Габрово" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Добрич" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Кърджали" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Кюстендил" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Ловеч" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Монтана" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Пазарджик" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Перник" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Плевен" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Пловдив" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Разград" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Русе" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Силистра" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Сливен" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Смолян" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област София-град" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Софийска област" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Стара Загора" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Търговище" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Хасково" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Шумен" });
            await dbContext.Regions.AddAsync(new Region { Country = countryBulgaria, Name = "Област Ямбол" });
        }
    }
}
