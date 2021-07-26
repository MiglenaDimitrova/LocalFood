namespace LocalFood.Web.Controllers
{
    using LocalFood.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProducersController : BaseController
    {
        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }
    }
}
