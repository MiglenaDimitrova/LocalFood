namespace LocalFood.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Moq;
    using Xunit;

    public class SearchServiceTests
    {
        [Fact]
        public void GetSearchedProductsByKeywordShouldWorkCorrectly()
        {
            var listProducers = new List<Producer>()
            {
                new Producer
                {
                    Id = 1, ApplicationUserId = "abv",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Location = new Location
                            {
                                Region = new Region { Name = "Герман" },
                                Adress = "Витоша 21",
                            },
                },
                new Producer
                {
                    Id = 2,
                    ApplicationUserId = "bvc",
                    FirstName = "Дончо",
                    LastName = "Георгиев",
                    Location = new Location
                    {
                        Region = new Region { Name = "Лозен" },
                        Adress = "Тинтява 27",
                    },
                },
            };
            var listCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Плодове" },
                new Category { Id = 2, Name = "Зеленчуци" },
                new Category { Id = 3, Name = "Млечни продукти" },
            };

            var listProducts = new List<Product>()
            {
              new Product
              {
                  Id = 1,
                  ProducerId = 1,
                  Name = "Зеле",
                  CategoryId = 2,
                  Category = listCategories[1],
                  Description = "ранно",
                  Price = 2.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = listProducers[0],
              },
              new Product
              {
                  Id = 2,
                  ProducerId = 1,
                  Name = "Домати",
                  CategoryId = 1,
                  Category = listCategories[0],
                  Description = "розови",
                  Price = 2.50m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = listProducers[0],
              },
              new Product
              {
                  Id = 3,
                  ProducerId = 1,
                  Name = "Краставици",
                  CategoryId = 2,
                  Category = listCategories[1],
                  Description = "сорт Гергана",
                  Price = 3.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = listProducers[0],
              },
              new Product
              {
                  Id = 4,
                  ProducerId = 2,
                  Name = "Моркови",
                  CategoryId = 2,
                  Category = listCategories[1],
                  Description = "бейби - за килограм",
                  Price = 5.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = listProducers[1],
              },
            };
            Mock<IDeletableEntityRepository<Category>> mockRepoCategories = new();
            Mock<IDeletableEntityRepository<Product>> mockRepoProducts = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoProducts.Setup(x => x.All()).Returns(listProducts.AsQueryable());
            var searchService = new SearchService(mockRepoProducts.Object);
            var result = searchService.GetSearchedProductsByKeyword("домат", 1,12).ToList();
            Assert.Equal(1, result.Count);
            Assert.Equal("Домати", result[0].Name);
        }
    }
}
