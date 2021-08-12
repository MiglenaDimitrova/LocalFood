namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LocalFood.Web.ViewModels.Producers;
    using LocalFood.Web.ViewModels.Products;

    public interface IProducersService
    {
        Task AddProducer(ProducerInputModel input, string userId, string imagePath);

        IEnumerable<ProducerViewModel> GetAllProducers(int page, int itemsPerPage);

        int ProducersCount();

        IEnumerable<CountryInputModel> GetCountryNames();

        Task AddProducerToUserCollection(string userId, int producerId);

        IEnumerable<ProducerViewModel> GetFavoriteProducers(string userId, int page, int itemsPerPage);

        int FavoriteProducersCount(string userId);

        Task DeleteFavorite(string userId, int producerId);

        string GetProducerUserId(int producerId);

        ProducerViewModel GetProducerById(int id);

        EditProducerInputModel GetMyProfile(int id);

        IEnumerable<OurProducerViewModel> GetOwrProducers();

        int GetProducerIdByUserId(string userId);

        Task UpdateProfileAsync(int id, EditProducerInputModel input);

        Task DeleteProfileAsync(int id);
    }
}
