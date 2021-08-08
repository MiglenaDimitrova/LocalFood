namespace LocalFood.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LocalFood.Common;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : BaseController
    {
        private readonly IRegionsService regionsService;

        public RegionsController(IRegionsService regionsService)
        {
            this.regionsService = regionsService;
        }

        [Authorize(Roles = GlobalConstants.ProducerRoleName)]
        public IEnumerable<RegionInputModel> Get([FromQuery]int id)
        {
            var regions = this.regionsService.GetRegionNamesByCountry(id);
            return regions;
        }
    }
}
