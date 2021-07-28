namespace LocalFood.Services.Data
{
    using System.Threading.Tasks;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Producers;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Producer> producerRepository;

        public ProducersService(IDeletableEntityRepository<Producer> producerRepository)
        {
            this.producerRepository = producerRepository;
        }

        public async Task AddProducer(ProducerInputModel input, string userId)
        {
            var location = new Location
            {
                CountryName = input.CountryName,
                Region = input.Region,
                LocalityName = input.LocalityName,
                Adress = input.Address,
                Longitude = input.Longitude,
                Latitude = input.Latitude,
            };

            var producer = new Producer
            {
                ApplicationUserId = userId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Image = input.Image,
                PhoneNumber = input.PhoneNumber,
                Site = input.Site,
                CompanyName = input.CompanyName,
                Description = input.Description,
                Email = input.Email,
                Location = location,
            };

            await this.producerRepository.AddAsync(producer);
            await this.producerRepository.SaveChangesAsync();
        }
    }
}
