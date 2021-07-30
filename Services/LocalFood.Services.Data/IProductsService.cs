namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task AddProduct(ProductInputModel input, string userId, string imagePath);

        ICollection<CategoryInputModel> GetCategories();

        IEnumerable<ProductViewModel> GetMyProducts(string userId, int page, int itemsPerPage);

        IEnumerable<ProductViewModel> GetAllProducts(int page, int itemsPerPage);

        int MyProductsCount(string userId);

        int ProductsCount();
    }
}
