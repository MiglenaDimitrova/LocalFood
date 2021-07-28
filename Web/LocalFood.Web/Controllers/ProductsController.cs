namespace LocalFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using LocalFood.Common;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productsService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public IActionResult Add()
        {
            var categories = this.productsService.GetCategories();

            var model = new ProductInputModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [HttpPost]

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public async Task<IActionResult> Add(ProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // from cookie- var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.userManager.GetUserAsync(this.User);
            await this.productsService.AddProduct(input, user.Id);
            return this.Redirect("/Products/MyProducts");
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public async Task<IActionResult> MyProducts(int id = 1)
        {
            if (id <= 0 )
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var user = await this.userManager.GetUserAsync(this.User);
            var model = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Products = this.productsService.GetMyProducts(user.Id, id, ItemsPerPage),
                ProductsCount = this.productsService.MyProductsCount(user.Id),
            };
            return this.View(model);
        }
    }
}
