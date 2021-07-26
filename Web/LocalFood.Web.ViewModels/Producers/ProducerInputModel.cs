﻿namespace LocalFood.Web.ViewModels.Producers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Models;

    public class ProducerInputModel
    {
        [Required]
        [Display(Name = "Име")]
        [MinLength(2, ErrorMessage = "Името трябва да съдържа поне 2 символа.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MinLength(3, ErrorMessage = "Фамилията трябва да съдържа поне 3 символа.")]
        public string LastName { get; set; }

        [Display(Name = "Име на вашата фирма, ферма, градинка... ")]
        [MinLength(3, ErrorMessage = "Името трябва да съдържа поне 3 символа.")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Телефонен номер")]
        [RegularExpression("[0-9]{10}")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Display(Name = "Сайт")]
        public string Site { get; set; }

        [Required]
        [MinLength(100)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Прикачи снимка")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Държава")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "Град")]
        public string TownName { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Географска дължина")]

        public double? Longitude { get; set; }

        [Display(Name = "Географска ширина")]

        public double? Latitude { get; set; }
    }
}
