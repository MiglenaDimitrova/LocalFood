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
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProductsServiceTests
    {
        private readonly ProductsService service;
        private readonly List<Producer> listProducers;
        private readonly List<Product> listProducts;
        private readonly List<Category> listCategories;

        public ProductsServiceTests()
        {
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
            this.listCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Плодове" },
                new Category { Id = 2, Name = "Зеленчуци" },
                new Category { Id = 3, Name = "Млечни продукти" },
            };

            this.listProducts = new List<Product>()
            {
              new Product
              {
                  Id = 1,
                  ProducerId = 1,
                  Name = "Зеле",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "ранно",
                  Price = 2.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 2,
                  ProducerId = 1,
                  Name = "Домати",
                  CategoryId = 1,
                  Category = this.listCategories[0],
                  Description = "розови",
                  Price = 2.50m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 3,
                  ProducerId = 1,
                  Name = "Краставици",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "сорт Гергана",
                  Price = 3.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 4,
                  ProducerId = 2,
                  Name = "Моркови",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "бейби - за килограм",
                  Price = 5.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[1],
              },
            };

            Mock<IDeletableEntityRepository<Category>> mockRepoCategories = new();
            Mock<IDeletableEntityRepository<Product>> mockRepoProducts = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoCategories.Setup(x => x.All()).Returns(this.listCategories.AsQueryable());
            mockRepoProducts.Setup(x => x.All()).Returns(this.listProducts.AsQueryable());
            mockRepoProducers.Setup(x => x.All()).Returns(this.listProducers.AsQueryable());
            this.service = new ProductsService(mockRepoProducts.Object, mockRepoCategories.Object, mockRepoProducers.Object);
        }

        [Fact]
        public async Task AddProductShouldWorkCorrectly()
        {
            var listProducts = new List<Product>()
            {
                new Product
                {
                    Id = 2,
                    ProducerId = 1,
                    Name = "Домати",
                    CategoryId = 1,
                    Description = "розови",
                    Price = 2.50m,
                    CreatedOn = DateTime.UtcNow,
                    Image = new Image { Id = "nnn", Extension = "png" },
                },
            };
            var listProducers = new List<Producer>() { new Producer { ApplicationUserId = "abv" } };
            var listCategories = new List<Category>() { new Category { Name = "Плодове" } };

            Mock<IDeletableEntityRepository<Product>> mockRepoProducts = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            Mock<IDeletableEntityRepository<Category>> mockRepoCategories = new();
            mockRepoProducts.Setup(x => x.All()).Returns(listProducts.AsQueryable());
            mockRepoProducts.Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Callback((Product product) => listProducts.Add(product));
            mockRepoProducers.Setup(x => x.All()).Returns(listProducers.AsQueryable());
            mockRepoCategories.Setup(x => x.All()).Returns(listCategories.AsQueryable());
            var newProduct = new ProductInputModel
            {
                Name = "Круши",
                Price = 3.00m,
                CategoryName = "Плодове",
                Categories = new List<CategoryInputModel>(),
                Description = "сладки",
                Image = null,
            };
            var service = new ProductsService(mockRepoProducts.Object, mockRepoCategories.Object, mockRepoProducers.Object);
            await service.AddProduct(newProduct, "abv", "_");
            var addedProduct = mockRepoProducts.Object.All().Where(x => x.Name == "Круши").FirstOrDefault();
            Assert.Equal("Круши", addedProduct.Name);
            Assert.Equal(3.00m, addedProduct.Price);
            Assert.Equal("сладки", addedProduct.Description);
            Assert.Equal(2, mockRepoProducts.Object.All().Count());
        }

        // In-Memory Database
        [Fact]
        public async Task ProductsCountShouldReturnCorrectValueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Products.Add(new Product());
            dbContext.Products.Add(new Product());
            dbContext.Products.Add(new Product());
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Product>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Producer>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new ProductsService(repository, repository3, repository2);
            Assert.Equal(3, service.ProductsCount());
        }

        [Fact]
        public void GetCategoriesShouldReturnCategories()
        {
            var result = this.service.GetCategories();

            Assert.Equal(3, result.Count);
            Assert.Equal("Плодове", result.First().CategoryName);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetMyProductsShouldReturnProductsOfCurrentUser()
        {
            var result = this.service.GetMyProducts("abv", 1, 12);

            Assert.Equal(3, this.service.MyProductsCount("abv"));
            Assert.Equal(3, result.ToList().Count);
            Assert.Equal("Домати", result.ToList()[1].Name);
            Assert.Equal(3.00m, result.ToList()[0].Price);
            Assert.Equal(2, result.ToList()[0].CategoryId);
        }

        [Fact]
        public void GetAllProductsShouldReturnCorrectResult()
        {
            var result = this.service.GetAllProducts(1, 12);

            Assert.Equal(4, result.ToList().Count);
            Assert.Equal("сорт Гергана", result.ToList()[1].Description);
            Assert.Equal(5.00m, result.ToList()[0].Price);
            Assert.Equal(1, result.ToList()[3].ProducerId);
            Assert.Equal("Моркови", result.ToList()[0].Name);
        }

        [Fact]
        public void GetProducerProductsAllShouldReturnCorrectResult()
        {
            var result1 = this.service.GetProducerProductsAll(1, 1, 12);
            var result2 = this.service.GetProducerProductsAll(2, 1, 12);

            Assert.Equal(3, result1.ToList().Count);
            Assert.Equal(1, result2.ToList().Count);
        }

        [Fact]
        public void GetProductByIdShouldReturnCorrectResult()
        {
            var result = this.service.GetProductById(1);
            var result1 = this.service.GetProductById(2);
            var result2 = this.service.GetProductById(3);
            var result3 = this.service.GetProductById(4);

            Assert.Equal(this.listProducts[0].Name, result.Name);
            Assert.Equal(this.listProducts[1].Name, result1.Name);
            Assert.Equal(this.listProducts[2].Description, result2.Description);
            Assert.Equal(this.listProducts[3].Price, result3.Price);
        }

        [Fact]
        public void GetUserIdByProductShouldReturnCorrectResult()
        {
            var result = this.service.GetUserIdByProduct(1);
            Assert.Equal("abv", result);
        }

        [Fact]
        public void GetNameByProducerIdShouldReturnFullName()
        {
            var result = this.service.GetNameByProducerId(1);
            Assert.Equal("Иван Иванов", result);
        }

        [Fact]
        public async Task DeleteProductAsyncShouldRemoveProduct()
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
                    Id = 2, ApplicationUserId = "bvc",
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
                  Category = this.listCategories[1],
                  Description = "ранно",
                  Price = 2.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 2,
                  ProducerId = 1,
                  Name = "Домати",
                  CategoryId = 1,
                  Category = this.listCategories[0],
                  Description = "розови",
                  Price = 2.50m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 3,
                  ProducerId = 1,
                  Name = "Краставици",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "сорт Гергана",
                  Price = 3.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 4,
                  ProducerId = 2,
                  Name = "Моркови",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "бейби - за килограм",
                  Price = 5.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[1],
              },
            };

            Mock<IDeletableEntityRepository<Category>> mockRepoCategories = new();
            Mock<IDeletableEntityRepository<Product>> mockRepoProducts = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoCategories.Setup(x => x.All()).Returns(listCategories.AsQueryable());
            mockRepoProducts.Setup(x => x.All()).Returns(listProducts.AsQueryable());
            mockRepoProducts.Setup(x => x.Delete(It.IsAny<Product>()))
                .Callback((Product product) => listProducts.Remove(product));
            mockRepoProducers.Setup(x => x.All()).Returns(listProducers.AsQueryable());
            var service = new ProductsService(mockRepoProducts.Object, mockRepoCategories.Object, mockRepoProducers.Object);
            await service.DeleteProductAsync(1);
            Assert.Equal(3, service.GetAllProducts(1, 12).Count());
        }

        [Fact]
        public async Task EditProductAsyncShouldEditProduct()
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
                    Id = 2, ApplicationUserId = "bvc",
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
                  Category = this.listCategories[1],
                  Description = "ранно",
                  Price = 2.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 2,
                  ProducerId = 1,
                  Name = "Домати",
                  CategoryId = 1,
                  Category = this.listCategories[0],
                  Description = "розови",
                  Price = 2.50m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 3,
                  ProducerId = 1,
                  Name = "Краставици",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "сорт Гергана",
                  Price = 3.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[0],
              },
              new Product
              {
                  Id = 4,
                  ProducerId = 2,
                  Name = "Моркови",
                  CategoryId = 2,
                  Category = this.listCategories[1],
                  Description = "бейби - за килограм",
                  Price = 5.00m,
                  CreatedOn = DateTime.UtcNow,
                  Image = new Image { Id = "nnn", Extension = "png" },
                  Producer = this.listProducers[1],
              },
            };

            var newProduct = new EditProductInputModel
            {
                Id = 3,
                Name = "Корнишон",
                Description = "сорт нов",
                Price = 3.50m,
                CategoryName = "Зеленчуци",
            };

            Mock<IDeletableEntityRepository<Category>> mockRepoCategories = new();
            Mock<IDeletableEntityRepository<Product>> mockRepoProducts = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoCategories.Setup(x => x.All()).Returns(listCategories.AsQueryable());
            mockRepoProducts.Setup(x => x.All()).Returns(listProducts.AsQueryable());
            mockRepoProducts.Setup(x => x.Update(It.IsAny<Product>()))
                .Callback((Product product) => UpdateProductInList(listProducts, product, newProduct));
            mockRepoProducers.Setup(x => x.All()).Returns(listProducers.AsQueryable());
            var service = new ProductsService(mockRepoProducts.Object, mockRepoCategories.Object, mockRepoProducers.Object);
            await service.UpdateProductAsync(3, newProduct);
            var productResult = mockRepoProducts.Object.All().FirstOrDefault(x => x.Id == 3);

            Assert.Equal("Корнишон", productResult.Name);
            Assert.Equal(3.50m, productResult.Price);
            Assert.Equal("сорт нов", productResult.Description);
        }

        private static List<Product> UpdateProductInList(List<Product> list, Product product, EditProductInputModel updatedProduct)
        {
            var neededProduct = list.FirstOrDefault(x => x.Id == product.Id);
            var index = list.IndexOf(neededProduct);
            list.RemoveAt(index);
            var category = new Category { Name = updatedProduct.CategoryName,  Id = 2 };
            var producer = new Producer
            {
                Id = 1,
                ApplicationUserId = "abv",
                FirstName = "Иван",
                LastName = "Иванов",
                Location = new Location
                {
                    Region = new Region { Name = "Герман" },
                    Adress = "Витоша 21",
                },
            };
            var updated = new Product
            {
                Id = 3,
                ProducerId = 1,
                Name = updatedProduct.Name,
                CategoryId = 2,
                Category = category,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Image = new Image { Id = "nnn", Extension = "png" },
                Producer = producer,
            };
            list.Insert(index, updated);
            return list;
        }
    }
}
