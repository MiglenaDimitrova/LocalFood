namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using LocalFood.Web.ViewModels.Producers;
    using LocalFood.Web.ViewModels.Products;

    public interface ISearchService
    {
        public IEnumerable<ProductViewModel> GetSearchedProductsByKeyword(string keyword);
    }
}
