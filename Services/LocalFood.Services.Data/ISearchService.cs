namespace LocalFood.Services.Data
{
    using LocalFood.Web.ViewModels.Producers;
    using LocalFood.Web.ViewModels.Products;

    public interface ISearchService
    {
        IEnumurable<ProductViewModel> GetSearchedProductsByKeyword(string keyword);
    }
}
