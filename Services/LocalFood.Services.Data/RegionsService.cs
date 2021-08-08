namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Producers;

    public class RegionsService : IRegionsService
    {
        private readonly IDeletableEntityRepository<Region> regionsRepository;

        public RegionsService(IDeletableEntityRepository<Region> regionsRepository)
        {
            this.regionsRepository = regionsRepository;
        }

        public IEnumerable<RegionInputModel> GetRegionNamesByCountry(int countryId)
        {
            return this.regionsRepository.All()
                 .Where(x => x.CountryId == countryId)
                 .Select(x => new RegionInputModel
                 {
                     Id = x.Id,
                     RegionName = x.Name,
                 }).ToList();
        }
    }
}
