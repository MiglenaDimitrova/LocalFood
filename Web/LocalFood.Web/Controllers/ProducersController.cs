namespace LocalFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using LocalFood.Common;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProducersController : BaseController
    {
        private readonly IProducersService producerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ProducersController(
            IProducersService producerService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.producerService = producerService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        public IActionResult Create()
        {
            return this.View();
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
            await this.producerService.AddProducer(input,user.Id);
            await this.userManager.RemoveFromRolesAsync(user, new List<string> { GlobalConstants.ProducerRoleName });
            await this.userManager.AddToRoleAsync(user,  GlobalConstants.ProducerWithProfileRoleName);

            await this.signInManager.SignOutAsync();
            await this.signInManager.SignInAsync(user, true);

            return this.Redirect("/Home/Index");
        }
    }
}
