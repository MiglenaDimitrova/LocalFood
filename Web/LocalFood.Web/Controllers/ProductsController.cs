namespace LocalFood.Web.Controllers
{
    using LocalFood.Common;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Add()
        {
            var categories = this.productsService.GetGategorires();

            var model = new ProductInputModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(ProductInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/Index");
        }

        public IActionResult MyProducts()
        {
            return this.Json("MyProducts");
        }
    }
}
