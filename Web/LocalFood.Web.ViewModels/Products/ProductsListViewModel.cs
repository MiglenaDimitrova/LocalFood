namespace LocalFood.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductsListViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
