namespace LocalFood.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class RegionsServiceTest
    {
        // In-Memory Database
        [Fact]
        public async Task MarketsCountShouldReturnCorrectValueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RegionsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Regions.Add(new Region());
            dbContext.Regions.Add(new Region());
            dbContext.Regions.Add(new Region());
            await dbContext.SaveChangesAsync();
            using var repository = new EfDeletableEntityRepository<Region>(dbContext);
            var serviceRegions = new RegionsService(repository);
            Assert.Equal(3, dbContext.Regions.Count());
        }

        [Fact]
        public void GetRegionNamesByCountry()
        {
            var listRegions = new List<Region>()
            {
                new Region
                {
                 Id = 1,
                 Name = "Софийска област",
                 Country = new Country { Id = 1, Name = "България", },
                 CountryId = 1,
                },
                new Region
                {
                 Id = 2,
                 Name = "Варненска област",
                 Country = new Country { Id = 1, Name = "България", },
                 CountryId = 1,
                },
                new Region
                {
                 Id = 3,
                 Name = "Солунски",
                 Country = new Country { Id = 2, Name = "Гърция", },
                 CountryId = 2,
                },
            };
            Mock<IDeletableEntityRepository<Region>> mockRepoRegions = new();
            mockRepoRegions.Setup(x => x.All()).Returns(listRegions.AsQueryable());
            var service = new RegionsService(mockRepoRegions.Object);
            var result = service.GetRegionNamesByCountry(1).Count();
            Assert.Equal(2, result);
        }
    }
}
