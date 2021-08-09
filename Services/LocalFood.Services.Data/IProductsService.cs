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

        IEnumerable<ProductViewModel> ProductsByUser(int producerId, int page, int itemsPerPage);

        EditProductInputModel GetProductById(int id);

        Task UpdateProductAsync(int id, EditProductInputModel input);

        Task DeleteProductAsync(int id);

        IEnumerable<ProductViewModel> GetProducerProductsAll(int producerId, int page, int itemsPerPage);

        int ProducerProductsCount(int producerId);

        public string GetNameByProducerId(int producerId);

        string GetUserIdByProduct(int id);
    }
}
