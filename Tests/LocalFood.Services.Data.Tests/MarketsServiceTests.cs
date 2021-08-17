namespace LocalFood.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LocalFood.Data;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Data.Repositories;
    using LocalFood.Web.ViewModels.Markets;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class MarketsServiceTests
    {
        // In-Memory Database
        [Fact]
        public async Task MarketsCountShouldReturnCorrectValueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MarketsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Markets.Add(new Market());
            dbContext.Markets.Add(new Market());
            dbContext.Markets.Add(new Market());
            dbContext.Markets.Add(new Market());
            dbContext.Markets.Add(new Market());
            await dbContext.SaveChangesAsync();
            using var repository = new EfDeletableEntityRepository<Market>(dbContext);
            var serviceMarkets = new MarketsService(repository);
            Assert.Equal(5, serviceMarkets.MarketsCount());
        }

        [Fact]
        public void GetAllMarkets()
        {
            var listMarkets = new List<Market>()
            {
                new Market
                {
                 Id = 1,
                 Name = "Женски",
                 Description = "mmm",
                 FullAddress = "Симеон 12",
                 UrlLocation = "google",
                 FacebookPage = "facebook",
                },
                new Market
                {
                 Id = 2,
                 Name = "Животински",
                 Description = "mmm",
                 FullAddress = "Мадрид",
                 UrlLocation = "google",
                 FacebookPage = "facebook",
                },
            };
            Mock<IDeletableEntityRepository<Market>> mockRepoMarkets = new();
            mockRepoMarkets.Setup(x => x.All()).Returns(listMarkets.AsQueryable());
            mockRepoMarkets.Setup(x => x.AddAsync(It.IsAny<Market>()))
                    .Callback((Market market) => listMarkets.Add(market));
            mockRepoMarkets.Setup(x => x.Delete(It.IsAny<Market>()))
                    .Callback((Market market) => listMarkets.Remove(market));
            var service = new MarketsService(mockRepoMarkets.Object);
            var result = service.GetAllMarkets(1, 12).Count();
            Assert.Equal(2, result);
        }
    }
}
