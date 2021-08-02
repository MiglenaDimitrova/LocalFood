namespace LocalFood.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using LocalFood.Common;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProducersController : BaseController
    {
        private const int ItemsPerPage = 12;
        private readonly IProducersService producersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment environment;

        public ProducersController(
            IProducersService producerService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment environment)
        {
            this.producersService = producerService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.environment = environment;
        }

        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        public IActionResult Create()
        {
            var countries = this.producersService.GetCountryNames();
            var model = new ProducerInputModel
            {
                Countries = countries,
            };
            var regions = this.producersService.GetRegionNamesByCountry();

            // AJAX for  country
            model.Regions = regions;
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(ProducerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.producersService.AddProducer(input, user.Id, $"{this.environment.ContentRootPath}/wwwroot/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View();
            }

            await this.userManager.RemoveFromRolesAsync(user, new List<string> { GlobalConstants.ProducerRoleName });
            await this.userManager.AddToRoleAsync(user,  GlobalConstants.ProducerWithProfileRoleName);

            await this.signInManager.SignOutAsync();
            await this.signInManager.SignInAsync(user, true);

            return this.Redirect("/Home/Index");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var model = new ProducersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Producers = this.producersService.GetAllProducers(id, ItemsPerPage),
                ItemsCount = this.producersService.ProducersCount(),
            };
            return this.View(model);
        }
    }
}
