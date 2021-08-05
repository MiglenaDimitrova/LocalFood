namespace LocalFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LocalFood.Common;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ConsumersController : BaseController
    {
        private const int ItemsPerPage = 12;
        private readonly IProducersService producersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ConsumersController(
            IProducersService producersService,
            UserManager<ApplicationUser> userManager)
        {
            this.producersService = producersService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Favorites(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var model = new ProducersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Producers = this.producersService.GetFavoriteProducers(user.Id, id, ItemsPerPage),
                ItemsCount = this.producersService.FavoriteProducersCount(user.Id),
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var producerUserId = this.producersService.GetProducerUserId(id);
            if (user.Id != producerUserId)
            {
                await this.producersService.AddProducerToUserCollection(user.Id, id);
            }

            return this.Redirect("/Consumers/Favorites");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.producersService.DeleteFavorite(user.Id, id);
            return this.Redirect("/Consumers/Favorites");
        }
    }
}
