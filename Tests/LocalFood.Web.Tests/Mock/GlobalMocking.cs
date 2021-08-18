namespace LocalFood.Web.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Producers;
    using LocalFood.Web.ViewModels.Products;

    public class GlobalMocking
    {
        public static ApplicationUser GetFakeUser()
        {
            return new ApplicationUser
            {
                Id = "abv",
            };
        }

        public static Producer GetFakeProducer()
        {
            var producer = new Producer
            {
                Id = 1,
                ApplicationUserId = "abv",
                FirstName = "Иван",
                LastName = "Иванов",
                Location = new Location
                {
                    Region = new Region { Name = "Софийска" },
                    Adress = "Витоша 21",
                    LocalityName = "Герман",
                },
                CompanyName = "Иван - градинка",
                Description = "В полите на Витоша",
                Email = string.Empty,
                Image = new Image { Extension = "png", Id = "abv" },
                LocationId = 1,
                PhoneNumber = "0888888888",
                Site = string.Empty,
                Votes = new List<Vote>() { new Vote { Value = 2 }, new Vote { Value = 4 }, },
            };
            return producer;
        }

        public static Product GetFakeProduct()
        {
            var product = new Product
            {
                Id = 1,
                ProducerId = 1,
                Name = "Зеле",
                CategoryId = 2,
                Category = new Category { Name = "Зеленчуци", Id = 2 },
                Description = "ранно",
                Price = 2.00m,
                CreatedOn = DateTime.UtcNow,
                Image = new Image { Id = "nnn", Extension = "png" },
                Producer = GetFakeProducer(),
            };
            return product;
        }

        public static ProductInputModel GetFakeAddProductModel()
        {
            var product = new ProductInputModel
            {
                Name = "Зеле",
                Description = "ранно",
                Price = 2.00m,
                Image = null,
                Categories = new List<CategoryInputModel>() { new CategoryInputModel { CategoryName = "Зеленчуци" } },
                CategoryName = "Зеленчуци",
            };
            return product;
        }

        public static ProducerInputModel GetFakeCreateProducerModel()
        {
            var producer = new ProducerInputModel
            {
                FirstName = "Пешо",
                LastName = "Петров",
                LocalityName = "Лозен",
                Address = "Витоша 21",
                RegionId = 1,
                CountryId = 1,
                CompanyName = "Пешо - градинка",
                Description = "био",
                Email = string.Empty,
                Image = null,
                PhoneNumber = "0888888888",
                Site = string.Empty,
                UrlLocation = string.Empty,
                Countries = new List<CountryInputModel>() { new CountryInputModel { CountryName = "България" }, },
            };
            return producer;
        }

        public static Market GetFakeMarket()
        {
            var market = new Market
            {
                Id = 1,
                Name = "Женски",
                Description = "всеки ден",
                FullAddress = "Симеон 12",
                FacebookPage = string.Empty,
                UrlLocation = string.Empty,
            };
            return market;
        }
    }
}
