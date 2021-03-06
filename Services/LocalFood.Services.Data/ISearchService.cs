namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using LocalFood.Web.ViewModels.Products;

    public interface ISearchService
    {
        public IEnumerable<ProductViewModel> GetSearchedProductsByKeyword(string keyword, int page, int itemsPerPage = 12);

        int GetSearchedProductsCount(string keyword);
    }
}
