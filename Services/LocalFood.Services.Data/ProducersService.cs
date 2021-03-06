namespace LocalFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Producers;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<Region> regionsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<UserProducer> usersProducersRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public ProducersService(
            IDeletableEntityRepository<Producer> producersRepository,
            IDeletableEntityRepository<Region> regionsRepository,
            IDeletableEntityRepository<UserProducer> usersProducersRepository,
            IDeletableEntityRepository<Country> countriesRepository,
            IDeletableEntityRepository<Location> locationsRepository)
        {
            this.producersRepository = producersRepository;
            this.countriesRepository = countriesRepository;
            this.regionsRepository = regionsRepository;
            this.usersProducersRepository = usersProducersRepository;
            this.locationsRepository = locationsRepository;
        }

        public async Task AddProducer(ProducerInputModel input, string userId, string imagePath)
        {
            var country = this.countriesRepository.All().FirstOrDefault(x => x.Id == input.CountryId);
            var region = this.regionsRepository.All().FirstOrDefault(x => x.Id == input.RegionId);
            var location = new Location
            {
                Country = country,
                Region = region,
                LocalityName = input.LocalityName,
                Adress = input.Address,
                UrlLocation = input.UrlLocation,
                Longitude = input.Longitude,
                Latitude = input.Latitude,
            };

            var producer = new Producer
            {
                ApplicationUserId = userId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber,
                Site = input.Site,
                CompanyName = input.CompanyName,
                Description = input.Description,
                Email = input.Email,
                Location = location,
            };
            Directory.CreateDirectory($"{imagePath}/producers/");
            if (input.Image == null)
            {
                producer.Image = null;
            }
            else
            {
                var extension = Path.GetExtension(input.Image.FileName);
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"{extension} e невалидно разширениe.");
                }

                var imageDb = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                producer.Image = imageDb;
                var physicalPath = $"{imagePath}/producers/{imageDb.Id}{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await input.Image.CopyToAsync(fileStream);
                }
            }

            await this.producersRepository.AddAsync(producer);
            await this.producersRepository.SaveChangesAsync();
        }

        public IEnumerable<ProducerViewModel> GetAllProducers(int page, int itemsPerPage)
        {
            return this.producersRepository.All()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProducerViewModel
                {
                    Id = x.Id,
                    FullName = $"{x.FirstName} {x.LastName}",
                    CompanyName = x.CompanyName,
                    Description = x.Description,
                    Email = x.Email,
                    FullAddress = $"{x.Location.Region.Name}, {x.Location.LocalityName}, {x.Location.Adress}",
                    PhoneNumber = x.PhoneNumber,
                    Site = x.Site,
                    Image = $"/images/producers/{x.Image.Id}{x.Image.Extension}",
                    UrlLocation = x.Location.UrlLocation,
                    CreatedOn = x.CreatedOn,
                    AverageVote = x.Votes.Average(x => x.Value).ToString("f1"),
                }).ToList();
        }

        public IEnumerable<CountryInputModel> GetCountryNames()
        {
            return this.countriesRepository.All()
                 .Select(x => new CountryInputModel
                 {
                    Id = x.Id,
                    CountryName = x.Name,
                 }).ToList();
        }

        public int ProducersCount()
        {
            return this.producersRepository.All().Count();
        }

        public async Task AddProducerToUserCollection(string userId, int producerId)
        {
            var userProducer = this.usersProducersRepository.All().FirstOrDefault(x => x.UserId == userId && x.ProducerId == producerId);
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == producerId);
            if (userProducer != null)
            {
                return;
            }

            userProducer = new UserProducer
            {
                UserId = userId,
                ProducerId = producerId,
                Producer = producer,
            };

            await this.usersProducersRepository.AddAsync(userProducer);
            await this.usersProducersRepository.SaveChangesAsync();
        }

        public IEnumerable<ProducerViewModel> GetFavoriteProducers(string userId, int page, int itemsPerPage = 12)
        {
            return this.usersProducersRepository.All().Where(x => x.UserId == userId)
                .Select(x => x.Producer)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProducerViewModel
                {
                    Id = x.Id,
                    FullName = $"{x.FirstName} {x.LastName}",
                    CompanyName = x.CompanyName,
                    Description = x.Description,
                    Email = x.Email,
                    FullAddress = $"{x.Location.Region.Name}, {x.Location.LocalityName}, {x.Location.Adress}",
                    PhoneNumber = x.PhoneNumber,
                    Site = x.Site,
                    Image = $"/images/producers/{x.Image.Id}{x.Image.Extension}",
                    UrlLocation = x.Location.UrlLocation,
                    CreatedOn = x.CreatedOn,
                    AverageVote = x.Votes.Average(x => x.Value).ToString("f1"),
                }).ToList();
        }

        public int FavoriteProducersCount(string userId)
        {
            return this.usersProducersRepository.All().Where(x => x.UserId == userId)
                .Select(x => x.Producer)
                .ToList().Count;
        }

        public async Task DeleteFavorite(string userId, int producerId)
        {
            var userProducer = this.usersProducersRepository.All().FirstOrDefault(x => x.UserId == userId && x.ProducerId == producerId);
            this.usersProducersRepository.Delete(userProducer);
            await this.usersProducersRepository.SaveChangesAsync();
        }

        public string GetProducerUserId(int producerId)
        {
            return this.producersRepository.All().FirstOrDefault(x => x.Id == producerId).ApplicationUserId;
        }

        public ProducerViewModel GetProducerById(int id)
        {
            return this.producersRepository.All().Where(x => x.Id == id)
                .Select(x => new ProducerViewModel
                {
                    Id = x.Id,
                    FullName = $"{x.FirstName} {x.LastName}",
                    CompanyName = x.CompanyName,
                    Description = x.Description,
                    Email = x.Email,
                    FullAddress = $"{x.Location.Region.Name}, {x.Location.LocalityName}, {x.Location.Adress}",
                    PhoneNumber = x.PhoneNumber,
                    Site = x.Site,
                    Image = $"/images/producers/{x.Image.Id}{x.Image.Extension}",
                    UrlLocation = x.Location.UrlLocation,
                    CreatedOn = x.CreatedOn,
                    AverageVote = x.Votes.Average(x => x.Value).ToString("f1"),
                }).FirstOrDefault();
        }

        public IEnumerable<OurProducerViewModel> GetOwrProducers()
        {
            return this.producersRepository.All()
                .Where(x => x.Votes.Count > 0)
                .Select(x => new OurProducerViewModel
                {
                    Id = x.Id,
                    FullName = $"{x.FirstName} {x.LastName}",
                    CompanyName = x.CompanyName,
                    Description = x.Description,
                    Email = x.Email,
                    FullAddress = $"{x.Location.Region.Name}, {x.Location.LocalityName}, {x.Location.Adress}",
                    PhoneNumber = x.PhoneNumber,
                    Site = x.Site,
                    Image = $"/images/producers/{x.Image.Id}{x.Image.Extension}",
                    UrlLocation = x.Location.UrlLocation,
                    CreatedOn = x.CreatedOn,
                    AverageVote = x.Votes.Average(x => x.Value).ToString("f1"),
                    AverargeVoteAsDouble = x.Votes.Average(x => x.Value),
                })
                .OrderByDescending(x => x.AverargeVoteAsDouble)
                .Take(6)
                .ToList();
        }

        public int GetProducerIdByUserId(string userId)
        {
            return this.producersRepository.All().FirstOrDefault(x => x.ApplicationUserId == userId).Id;
        }

        public EditProducerInputModel GetMyProfile(int id)
        {
            return this.producersRepository.All().Where(x => x.Id == id)
                .Select(x => new EditProducerInputModel
                {
                    CompanyName = x.CompanyName,
                    Description = x.Description,
                    Email = x.Email,
                    Address = x.Location.Adress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LocalityName = x.Location.LocalityName,
                    PhoneNumber = x.PhoneNumber,
                    Site = x.Site,
                    UrlLocation = x.Location.UrlLocation,
                }).FirstOrDefault();
        }

        public async Task UpdateProfileAsync(int id, EditProducerInputModel input)
        {
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == id);
            var location = this.locationsRepository.All().FirstOrDefault(x => x.Id == producer.LocationId);
            producer.FirstName = input.FirstName;
            producer.LastName = input.LastName;
            producer.PhoneNumber = input.PhoneNumber;
            producer.CompanyName = input.CompanyName;
            producer.Description = input.Description;
            producer.Site = input.Site;
            location.Adress = input.Address;
            location.LocalityName = input.LocalityName;
            location.CountryId = input.CountryId;
            location.RegionId = input.RegionId;
            location.UrlLocation = input.UrlLocation;

            await this.locationsRepository.SaveChangesAsync();
            await this.producersRepository.SaveChangesAsync();
        }

        public async Task DeleteProfileAsync(int id)
        {
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == id);
            this.producersRepository.Delete(producer);
            await this.producersRepository.SaveChangesAsync();
        }
    }
}
