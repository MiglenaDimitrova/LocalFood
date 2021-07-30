namespace LocalFood.Web.ViewModels.Producers
{
    using System.Collections.Generic;

    public class ProducersListViewModel : PagingViewModel
    {
        public IEnumerable<ProducerViewModel> Producers { get; set; }
    }
}
