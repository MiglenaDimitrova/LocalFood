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
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProducersServiceTests
    {
        private readonly ProducersService service;
        private readonly List<UserProducer> listUsersProducers;
        private readonly List<Producer> listProducers;
        private readonly List<Country> listCountries;

        public ProducersServiceTests()
        {
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            Mock<IDeletableEntityRepository<Country>> mockRepoCountries = new();
            Mock<IDeletableEntityRepository<Location>> mockRepoLocation = new();
            Mock<IDeletableEntityRepository<Region>> mockRepoRegions = new();
            Mock<IDeletableEntityRepository<UserProducer>> mockRepoUserProducer = new();
            this.service = new ProducersService(
                mockRepoProducers.Object,
                mockRepoRegions.Object,
                mockRepoUserProducer.Object,
                mockRepoCountries.Object,
                mockRepoLocation.Object);
            this.listUsersProducers = new List<UserProducer>()
            {
                new UserProducer
                {
                    UserId = "abv",
                    Producer = new Producer
                    {
                    Id = 2,
                    ApplicationUserId = "bcd",
                    FirstName = "Добри",
                    LastName = "Иванов",
                    Location = new Location
                            {
                                Region = new Region { Name = "Софийска" },
                                Adress = "Витоша 21",
                                LocalityName = "Герман",
                            },
                    CompanyName = "Добри - градинка",
                    Description = "В полите на Витоша",
                    Email = string.Empty,
                    Image = new Image { Extension = "png", Id = "bcd" },
                    LocationId = 1,
                    PhoneNumber = "0888888888",
                    Site = string.Empty,
                    Votes = new List<Vote>() { new Vote { Value = 2 }, new Vote { Value = 4 }, },
                    },
                    ProducerId = 2,
                },
            };

            this.listCountries = new List<Country>()
            {
                new Country { Name = "България", Id = 1, },
                new Country { Name = "Гърция", Id = 2, },
            };

            this.listProducers = new List<Producer>()
            {
                new Producer
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
                },
                new Producer
                {
                    Id = 2,
                    ApplicationUserId = "bcd",
                    FirstName = "Добри",
                    LastName = "Иванов",
                    Location = new Location
                            {
                                Region = new Region { Name = "Софийска" },
                                Adress = "Витоша 21",
                                LocalityName = "Герман",
                            },
                    CompanyName = "Добри - градинка",
                    Description = "В полите на Витоша",
                    Email = string.Empty,
                    Image = new Image { Extension = "png", Id = "bcd" },
                    LocationId = 1,
                    PhoneNumber = "0888888888",
                    Site = string.Empty,
                    Votes = new List<Vote>() { new Vote { Value = 2 }, new Vote { Value = 4 }, },
                },
            };
            mockRepoCountries.Setup(x => x.All()).Returns(this.listCountries.AsQueryable());
            mockRepoUserProducer.Setup(x => x.All()).Returns(this.listUsersProducers.AsQueryable());
            mockRepoUserProducer.Setup(x => x.AddAsync(It.IsAny<UserProducer>()))
                .Callback((UserProducer userProducer) => this.listUsersProducers.Add(userProducer));
            mockRepoUserProducer.Setup(x => x.Delete(It.IsAny<UserProducer>()))
                .Callback((UserProducer userProducer) => this.listUsersProducers.Remove(userProducer));
            mockRepoProducers.Setup(x => x.All()).Returns(this.listProducers.AsQueryable());
            mockRepoProducers.Setup(x => x.AddAsync(It.IsAny<Producer>()))
                .Callback((Producer producer) => this.listProducers.Add(producer));
            mockRepoProducers.Setup(x => x.Delete(It.IsAny<Producer>()))
                .Callback((Producer producer) => this.listProducers.Remove(producer));
        }

        // In-Memory Database
        [Fact]
        public async Task ProductsCountShouldReturnCorrectValueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Producers.Add(new Producer());
            dbContext.Producers.Add(new Producer());
            dbContext.Producers.Add(new Producer());
            dbContext.Producers.Add(new Producer());
            await dbContext.SaveChangesAsync();

            using var repository4 = new EfDeletableEntityRepository<Country>(dbContext);
            using var repository = new EfDeletableEntityRepository<Producer>(dbContext);
            using var repository5 = new EfDeletableEntityRepository<Location>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<UserProducer>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Region>(dbContext);
            var service = new ProducersService(repository, repository2, repository3, repository4, repository5);
            Assert.Equal(4, service.ProducersCount());
        }

        [Fact]
        public async Task AddProducerShouldWorkCorrectly()
        {
            var newProducer = new ProducerInputModel
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
            };
            await this.service.AddProducer(newProducer, "abv", "_");
            var producer = this.listProducers.FirstOrDefault(x => x.FirstName == "Пешо");
            Assert.Equal(3, this.listProducers.Count);
            Assert.Equal("Пешо", producer.FirstName);
            Assert.Equal("био", producer.Description);
        }

        [Fact]
        public void GetAllProducersShouldWorkCorrectly()
        {
            var result = this.service.GetAllProducers(1, 12);
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetCountryNamesShouldWorkCorrectly()
        {
            var result = this.service.GetCountryNames().ToList();
            Assert.Equal(2, result.Count);
            Assert.Equal("България", result[0].CountryName);
        }

        [Fact]
        public void GetFavoriteProducersShouldWorkCorrectly()
        {
            var result = this.service.GetFavoriteProducers("abv", 1, 12).ToList();
            Assert.Equal("Добри Иванов", result[0].FullName);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task AddProducerToUserCollectionShouldWorkCorrectly()
        {
            await this.service.AddProducerToUserCollection("bcd", 1);
            Assert.Equal(2, this.listUsersProducers.Count);
        }

        //[Fact]
        //public void FavoriteProducersCountShouldWorkCorrectly()
        //{
        //    var result = this.service.FavoriteProducersCount("abc");
        //    Assert.Equal(1, result);
        //}

        [Fact]
        public async Task DeleteFavoriteShouldWorkCorrectly()
        {
            await this.service.DeleteFavorite("abv", 2);
            Assert.Equal(0, listUsersProducers.Count);
        }

        [Fact]
        public void GetProducerUserIdShouldWorkCorrectly()
        {
            var result = this.service.GetProducerUserId(2);
            Assert.Equal("bcd", result);
        }

        [Fact]
        public void GetProducerByIdShouldWorkCorrectly()
        {
            var result = this.service.GetProducerById(2);
            Assert.Equal("Добри Иванов", result.FullName);
        }

        [Fact]
        public void GetOwrProducersShouldWorkCorrectly()
        {
            var result = this.service.GetOwrProducers();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetProducerIdByUserIdShouldWorkCorrectly()
        {
            var result = this.service.GetProducerIdByUserId("abv");
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetMyProfileShouldWorkCorrectly()
        {
            var result = this.service.GetMyProfile(1);
            Assert.Equal("Иван", result.FirstName);
            Assert.Equal("Иванов", result.LastName);
            Assert.Equal("В полите на Витоша", result.Description);
        }

        [Fact]
        public async Task DeleteProfileAsyncWorkCorrectly()
        {
            await this.service.DeleteProfileAsync(1);
            Assert.Equal(1, this.listProducers.Count());
        }
    }
}
