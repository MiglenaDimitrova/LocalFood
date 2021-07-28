namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Services.Mapping;
    using LocalFood.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Producer> producerRepository;

        public ProductsService(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Producer> producerRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.producerRepository = producerRepository;
        }

        public async Task AddProduct(ProductInputModel input, string userId)
        {
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Name == input.CategoryName);
            var producer = this.producerRepository.All().FirstOrDefault(x => x.ApplicationUserId == userId);
            var product = new Product
            {
                Name = input.Name,
                Category = category,
                ProducerId = producer.Id,
                Description = input.Description,
                Image = input.Image,
                Price = input.Price,
            };
            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public ICollection<CategoryInputModel> GetCategories()
        {
            return this.categoryRepository.All().ToList()
                .Select(x => new CategoryInputModel
                {
                    CategoryName = x.Name,
                })
                .ToList();
        }

        public IEnumerable<ProductViewModel> GetMyProducts(string userId, int page, int itemsPerPage = 12)
        {
            var producer = this.producerRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == userId);
            return this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Where(x => x.ProducerId == producer.Id)
                .To<ProductViewModel>()
                .ToList();
        }

        public int MyProductsCount(string userId)
        {
            var producer = this.producerRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == userId);
            return this.productRepository.AllAsNoTracking()
                .Where(x => x.ProducerId == producer.Id)
                .Count();
        }
    }
}
