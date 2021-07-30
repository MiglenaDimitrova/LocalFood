namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LocalFood.Web.ViewModels.Producers;

    public interface IProducersService
    {
        Task AddProducer(ProducerInputModel input, string userId, string imagePath);

        IEnumerable<ProducerViewModel> GetAllProducers(int page, int itemsPerPage);

        int ProducersCount();
    }
}
