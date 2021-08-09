namespace LocalFood.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProducerProductsListViewModel : PagingViewModel
    {
        public string ProducerName { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
