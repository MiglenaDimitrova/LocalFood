namespace LocalFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Markets;

    public class MarketsService : IMarketsService
    {
        private readonly IDeletableEntityRepository<Market> marketsRepository;

        public MarketsService(IDeletableEntityRepository<Market> marketsRepository)
        {
            this.marketsRepository = marketsRepository;
        }

        public IEnumerable<MarketViewModel> GetAllMarkets(int id, int itemsPerPage)
        {
            return this.marketsRepository.All()
                .Select(x => new MarketViewModel
                {
                    Name = x.Name,
                    FullAddress = x.FullAddress,
                    Description = x.Description,
                    FacebookPage = x.FacebookPage,
                    UrlLocation = x.UrlLocation,
                }).ToList();
        }

        public int MarketsCount()
        {
            return this.marketsRepository.AllAsNoTracking().Count();
        }
    }
}
