namespace LocalFood.Web.ViewModels.Markets
{
    using System.Collections.Generic;

    public class MarketsListViewModel : PagingViewModel
    {
        public IEnumerable<MarketViewModel> Markets { get; set; }
    }
}
