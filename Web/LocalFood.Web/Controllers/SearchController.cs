namespace LocalFood.Web.Controllers
{
    using System.Linq;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private const int ItemsPerPage = 12;
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IActionResult ProductsByKeyword(string keyword, int id = 1)
        {
            if (string.IsNullOrWhiteSpace(keyword) && id == 1)
            {
                return this.Redirect("/Home/Index");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            var products = this.searchService.GetSearchedProductsByKeyword(keyword, id, ItemsPerPage);
            var itemsCount = this.searchService.GetSearchedProductsCount(keyword);
            var model = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Products = products,
                ItemsCount = itemsCount,
            };
            return this.View(model);
        }
    }
}
