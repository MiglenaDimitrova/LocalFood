namespace LocalFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Services.Mapping;
    using LocalFood.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Producer> producersRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;
            this.producersRepository = producersRepository;
        }

        public async Task AddProduct(ProductInputModel input, string userId, string imagePath)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Name == input.CategoryName);
            var producer = this.producersRepository.All().FirstOrDefault(x => x.ApplicationUserId == userId);
            var product = new Product
            {
                Name = input.Name,
                Category = category,
                ProducerId = producer.Id,
                Description = input.Description,
                Price = input.Price,
                IsBio = bool.Parse(input.IsBio),
            };
            if (input.Image == null)
            {
                product.Image = null;
            }
            else
            {
                Directory.CreateDirectory($"{imagePath}/products/");
                var extension = Path.GetExtension(input.Image.FileName);
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"{extension} e невалидно разширениe.");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                product.Image = dbImage;

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await input.Image.CopyToAsync(fileStream);
                }
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public ICollection<CategoryInputModel> GetCategories()
        {
            return this.categoriesRepository.All().ToList()
                .Select(x => new CategoryInputModel
                {
                    CategoryName = x.Name,
                })
                .ToList();
        }

        public IEnumerable<ProductViewModel> GetMyProducts(string userId, int page, int itemsPerPage = 12)
        {
            var producer = this.producersRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == userId);
            return this.productsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Where(x => x.ProducerId == producer.Id)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    IsBio = x.IsBio,
                    Price = x.Price,
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    Name = x.Name,
                    Image = $"/images/products/{x.Image.Id}.{x.Image.Extension}",
                    ProducerId = x.ProducerId,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                })
                .ToList();
        }

        public IEnumerable<ProductViewModel> GetAllProducts(int page, int itemsPerPage = 12)
        {
            return this.productsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    IsBio = x.IsBio,
                    Price = x.Price,
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    Name = x.Name,
                    Image = $"/images/products/{x.Image.Id}.{x.Image.Extension}",
                    ProducerId = x.ProducerId,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                    UrlLocation = x.Producer.Location.UrlLocation,
                })
                .ToList();
        }

        public int MyProductsCount(string userId)
        {
            var producer = this.producersRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == userId);
            return this.productsRepository.AllAsNoTracking()
                .Where(x => x.ProducerId == producer.Id)
                .Count();
        }

        public int ProductsCount()
        {
            return this.productsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<ProductViewModel> ProductsByUser(int producerId, int page, int itemsPerPage = 12)
        {
            return this.productsRepository.AllAsNoTracking()
                .Where(x => x.ProducerId == producerId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProductViewModel
                {
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    IsBio = x.IsBio,
                    Price = x.Price,
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    Name = x.Name,
                    Image = $"/images/products/{x.Image.Id}.{x.Image.Extension}",
                    ProducerId = x.ProducerId,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                    UrlLocation = x.Producer.Location.UrlLocation,
                })
                .ToList();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            this.productsRepository.Delete(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public EditProductInputModel GetProductById(int id)
        {
            return this.productsRepository.All()
               .Where(x => x.Id == id)
               .Select(x => new EditProductInputModel
               {
                   Description = x.Description,
                   Name = x.Name,
                   IsBio = x.IsBio.ToString(),
                   Price = x.Price,
                   CategoryName = x.Category.Name,
               }).FirstOrDefault();
        }

        public async Task UpdateProductAsync(int id, EditProductInputModel input)
        {
            var product = this.productsRepository.All()
               .Where(x => x.Id == id).FirstOrDefault();
            product.Name = input.Name;
            product.Description = input.Description;
            product.IsBio = bool.Parse(input.IsBio);
            product.Price = input.Price;
            product.Category = this.categoriesRepository.All().FirstOrDefault(x => x.Name == input.CategoryName);

            await this.productsRepository.SaveChangesAsync();
        }

        public IEnumerable<ProductViewModel> GetProducerProductsAll(int producerId, int page, int itemsPerPage = 12)
        {
            return this.productsRepository.All()
                .Where(x => x.ProducerId == producerId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProductViewModel
                {
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    IsBio = x.IsBio,
                    Price = x.Price,
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    Name = x.Name,
                    Image = $"/images/products/{x.Image.Id}.{x.Image.Extension}",
                    ProducerId = x.ProducerId,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                    UrlLocation = x.Producer.Location.UrlLocation,
                }).ToList();
        }

        public int ProducerProductsCount(int producerId)
        {
            return this.producersRepository.All().FirstOrDefault(x => x.Id == producerId).Products.Count;
        }

        public string GetNameByProducerId(int producerId)
        {
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == producerId);
            return $"{producer.FirstName} {producer.LastName}";
        }

        public string GetUserIdByProduct(int id)
        {
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == product.ProducerId);
            return producer.ApplicationUserId;
        }
    }
}
