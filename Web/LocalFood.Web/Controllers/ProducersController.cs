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
    using LocalFood.Web.ViewModels.Products;
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
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(ProducerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var countries = this.producersService.GetCountryNames();
                var model = new ProducerInputModel
                {
                    Countries = countries,
                };
                return this.View(model);
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

        public IActionResult Details(int id)
        {
            var model = this.producersService.GetProducerById(id);
            return this.View(model);
        }

        [Authorize(Roles= GlobalConstants.ProducerWithProfileRoleName)]
        public IActionResult MyProfile()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var producerId = this.producersService.GetProducerIdByUserId(userId);
            var model = this.producersService.GetProducerById(producerId);
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        public IActionResult Edit(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var producerUserId = this.producersService.GetProducerUserId(id);
            if (userId != producerUserId)
            {
                return this.Forbid();
            }

            var model = this.producersService.GetMyProfile(id);
            var countries = this.producersService.GetCountryNames();
            model.Countries = countries;

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProducerInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var producerUserId = this.producersService.GetProducerUserId(id);
            if (userId != producerUserId)
            {
                return this.Forbid();
            }

            if (!this.ModelState.IsValid)
            {
                var model = this.producersService.GetMyProfile(id);
                var countries = this.producersService.GetCountryNames();
                model.Countries = countries;
            }

            await this.producersService.UpdateProfileAsync(id, input);
            return this.Redirect("/Producers/MyProfile");
        }

        [Authorize(Roles = GlobalConstants.ProducerWithProfileRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var producerUserId = this.producersService.GetProducerUserId(id);
            if (userId != producerUserId)
            {
                return this.Forbid();
            }

            await this.producersService.DeleteProfileAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, GlobalConstants.ProducerWithProfileRoleName);
            await this.userManager.AddToRoleAsync(user, GlobalConstants.ProducerRoleName);

            await this.signInManager.SignOutAsync();
            await this.signInManager.SignInAsync(user, true);

            return this.Redirect("/Home/Index");
        }
    }
}
