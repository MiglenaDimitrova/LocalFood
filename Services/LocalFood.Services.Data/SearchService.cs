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

        public IEnumurable<ProductViewModel> GetSearchedProductsByKeyword(string keyword)
        {
            var productsWithKeyword = new List<ProductViewModel>();
            var keywordToLower = keyword.ToLower();
            var products = this.productsRepository.All().ToList();
            var productsForSearch = products.Select(x => new ProductViewModel
            {
                Name = x.Name,
                FullAddress = $"{x.Producer.Location.Region.Name}{x.Producer.Location.Adress}",
                ProducerName = $"{x.Producer.FirstName} {x.Producer.LastName}",
                CategoryName = x.Category.Name,
            });
            foreach (var product in productsForSearch)
            {
                var name = product.Name.ToLower();
                var address = product.FullAddress.ToLower();
                var producerName = product.ProducerName.ToLower();
                var category = product.CategoryName.ToLower();
                List<string> propertiesToSearch = new List<string>() { name, address, producerName, category };
                foreach (var item in propertiesToSearch)
                {
                    if (item.Contains(keywordToLower))
                    {
                        productsWithKeyword.Add(product);
                    }
                }
            }

            return (IEnumurable<ProductViewModel>)productsWithKeyword;
        }
    }
}
