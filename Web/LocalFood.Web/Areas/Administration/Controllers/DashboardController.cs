namespace LocalFood.Web.Areas.Administration.Controllers
{
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
