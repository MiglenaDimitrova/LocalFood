namespace LocalFood.Web.ViewModels.Producers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditProducerInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Име")]
        [MinLength(2, ErrorMessage = "Името трябва да съдържа поне 2 символа.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Фамилия")]
        [MinLength(3, ErrorMessage = "Фамилията трябва да съдържа поне 3 символа.")]
        public string LastName { get; set; }

        [Display(Name = "Име на вашата фирма, ферма, градинка... ")]
        [MinLength(3, ErrorMessage = "Името трябва да съдържа поне 3 символа.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Телефонен номер")]
        [RegularExpression("[0-9]{10}")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Display(Name = "Сайт")]
        public string Site { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [MinLength(10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Държава")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Област")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Населено място")]
        public string LocalityName { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "")]
        public string UrlLocation { get; set; }

        public IEnumerable<CountryInputModel> Countries { get; set; }
    }
}
