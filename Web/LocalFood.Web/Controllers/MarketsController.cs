namespace LocalFood.Web.Controllers
{
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Markets;
    using Microsoft.AspNetCore.Mvc;

    public class MarketsController : BaseController
    {
        private const int ItemsPerPage = 12;
        private readonly IMarketsService marketsService;

        public MarketsController(IMarketsService marketsService)
        {
            this.marketsService = marketsService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var model = new MarketsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Markets = this.marketsService.GetAllMarkets(id, ItemsPerPage),
                ItemsCount = this.marketsService.MarketsCount(),
            };
            return this.View(model);
        }
    }
}
