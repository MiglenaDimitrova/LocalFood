namespace LocalFood.Web.Controllers
{
    using System.Diagnostics;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IProducersService producersService;

        public HomeController(IProducersService producersService)
        {
            this.producersService = producersService;
        }

        public IActionResult Index()
        {
            var model = new OurProducersViewModel
            {
                Producers = this.producersService.GetOwrProducers(),
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
