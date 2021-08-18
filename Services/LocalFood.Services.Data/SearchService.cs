namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Products;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public SearchService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IEnumerable<ProductViewModel> GetSearchedProductsByKeyword(string keyword, int page, int itemsPerPage)
        {
            var productsWithKeyword = new HashSet<ProductViewModel>();
            var keywordToLower = keyword.ToLower();
            var products = this.productsRepository.All()
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    CategoryName = x.Category.Name,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    ProducerId = x.ProducerId,
                    UrlLocation = x.Producer.Location.UrlLocation,
                    Image = $"/images/products/{x.Image.Id}{x.Image.Extension}",
                }).ToList();

            foreach (var product in products)
            {
                var name = product.Name.ToLower();
                var address = product.FullAddress.ToLower();
                var producerName = product.ProducerName.ToLower();
                var category = product.CategoryName.ToLower();
                List<string> propertiesToSearch = new() { name, address, producerName, category };
                foreach (var item in propertiesToSearch)
                {
                    if (item.Contains(keywordToLower))
                    {
                        productsWithKeyword.Add(product);
                    }
                }
            }

            return productsWithKeyword
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(x => x).ToList();
        }

        public int GetSearchedProductsCount(string keyword)
        {
            var productsWithKeyword = new HashSet<ProductViewModel>();
            var keywordToLower = keyword.ToLower();
            var products = this.productsRepository.All()
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    FullAddress = $"{x.Producer.Location.Region.Name}, {x.Producer.Location.Adress}",
                    ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                    CategoryName = x.Category.Name,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    ProducerId = x.ProducerId,
                    UrlLocation = x.Producer.Location.UrlLocation,
                    Image = $"/images/products/{x.Image.Id}.{x.Image.Extension}",
                }).ToList();

            foreach (var product in products)
            {
                var name = product.Name.ToLower();
                var address = product.FullAddress.ToLower();
                var producerName = product.ProducerName.ToLower();
                var category = product.CategoryName.ToLower();
                List<string> propertiesToSearch = new() { name, address, producerName, category };
                foreach (var item in propertiesToSearch)
                {
                    if (item.Contains(keywordToLower))
                    {
                        productsWithKeyword.Add(product);
                    }
                }
            }

            return productsWithKeyword.Count;
        }
    }
}
