﻿namespace LocalFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Http;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public ProducersService(IDeletableEntityRepository<Producer> producerRepository)
        {
            this.producersRepository = producerRepository;
        }

        public async Task AddProducer(ProducerInputModel input, string userId, string imagePath)
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
                PhoneNumber = input.PhoneNumber,
                Site = input.Site,
                CompanyName = input.CompanyName,
                Description = input.Description,
                Email = input.Email,
                Location = location,
            };
            Directory.CreateDirectory($"{imagePath}/producers/");
            var extension = Path.GetExtension(input.Image.FileName);
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"{extension} e невалидно разширениe.");
            }

            var dbImage = new Image
            {
                AddedByUserId = userId,
                Extension = extension,
            };
            producer.Image = dbImage;
            var physicalPath = $"{imagePath}/producers/{dbImage.Id}.{extension}";
            using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                await input.Image.CopyToAsync(fileStream);
            }

            await this.producersRepository.AddAsync(producer);
            await this.producersRepository.SaveChangesAsync();
        }

        public IEnumerable<ProducerViewModel> GetAllProducers(int page, int itemsPerPage = 12)
        {
            return this.producersRepository.All()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProducerViewModel
                {
                     FullName = $"{x.FirstName} {x.LastName}",
                     CompanyName = x.CompanyName,
                     Description = x.Description,
                     Email = x.Email,
                     FullAddress = $"{x.Location.Region}, {x.Location.LocalityName}, {x.Location.Adress}",
                     PhoneNumber = x.PhoneNumber,
                     Site = x.Site,
                     Image = $"/images/producers/{x.Image.Id}.{x.Image.Extension}",
                }).ToList();
        }

        public int ProducersCount()
        {
            return this.producersRepository.All().Count();
        }
    }
}
