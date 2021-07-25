namespace LocalFood.Web.Controllers
{
    using LocalFood.Common;
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
    public class ProductsController : BaseController
    {
        public IActionResult Add()
        {
            return this.View();
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
