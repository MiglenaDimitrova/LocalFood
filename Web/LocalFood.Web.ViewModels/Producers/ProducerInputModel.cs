namespace LocalFood.Web.ViewModels.Producers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class ProducerInputModel
    {
        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Име")]
        [MinLength(2, ErrorMessage = "Името трябва да съдържа поне 2 символа.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Фамилия")]
        [MinLength(3, ErrorMessage = "Фамилията трябва да съдържа поне 3 символа.")]
        public string LastName { get; set; }

        [Display(Name = "Име на вашата фирма, ферма, градинка... ")]
        [MinLength(3, ErrorMessage = "Името трябва да съдържа поне 3 символа.")]
        public string CompanyName { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Телефонен номер")]
        [RegularExpression("[0-9]{10}")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Невалиден имейл")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Display(Name = "Сайт")]
        public string Site { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [MinLength(10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Държава")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Област")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Населено място")]
        public string LocalityName { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "")]
        [Url(ErrorMessage = "Невалиден линк")]
        public string UrlLocation { get; set; }

        [Display(Name = "Географска дължина")]

        public double? Longitude { get; set; }

        [Display(Name = "Географска ширина")]

        public double? Latitude { get; set; }

        public IEnumerable<CountryInputModel> Countries { get; set; }

        public IEnumerable<RegionInputModel> Regions { get; set; }
    }
}
