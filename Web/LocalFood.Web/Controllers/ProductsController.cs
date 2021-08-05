namespace LocalFood.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using LocalFood.Common;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private const int ItemsPerPage = 12;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ProductsController(
            IProductsService productsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.productsService = productsService;
            this.userManager = userManager;
            this.environment = environment;
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
                var categories = this.productsService.GetCategories();

                var model = new ProductInputModel
                {
                    Categories = categories,
                };

                return this.View(model);
            }

            // from cookie- var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.productsService.AddProduct(input, user.Id, $"{this.environment.ContentRootPath}/wwwroot/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var categories = this.productsService.GetCategories();

                var model = new ProductInputModel
                {
                    Categories = categories,
                };

                return this.View(model);
            }

            return this.Redirect("/Products/MyProducts");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var model = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Products = this.productsService.GetAllProducts(id, ItemsPerPage),
                ItemsCount = this.productsService.ProductsCount(),
            };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public async Task<IActionResult> MyProducts(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var model = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Products = this.productsService.GetMyProducts(user.Id, id, ItemsPerPage),
                ItemsCount = this.productsService.MyProductsCount(user.Id),
            };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public IActionResult Edit(int id)
        {
            var categories = this.productsService.GetCategories();
            var model = this.productsService.GetProductById(id);
            model.Categories = categories;
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.productsService.GetCategories();

                var model = new EditProductInputModel
                {
                    Categories = categories,
                };

                return this.View(model);
            }

            await this.productsService.UpdateProductAsync(id, input);
            return this.Redirect("/Products/MyProducts");
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.productsService.DeleteProductAsync(id);
            return this.Redirect("/Products/MyProducts");
        }

        public IActionResult ProducerProductsAll(int producerId, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var model = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Products = this.productsService.GetProducerProductsAll(producerId, id, ItemsPerPage),
                ItemsCount = this.productsService.ProducerProductsCount(producerId),
            };
            return this.View(model);
        }
    }
}
