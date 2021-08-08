namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LocalFood.Web.ViewModels.Markets;

    public interface IMarketsService
    {
        IEnumerable<MarketViewModel> GetAllMarkets(int id, int itemsPerPage);

        int MarketsCount();
    }
}
