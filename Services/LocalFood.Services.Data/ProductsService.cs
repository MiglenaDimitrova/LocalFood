namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
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
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Name == input.Category);
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

        public IEnumerable<SelectListItem> GetGategorires()
        {
            var categories = this.categoryRepository.All().ToList();
            IEnumerable<SelectListItem> sliCategories = categories
                .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return sliCategories;
        }
    }
}
