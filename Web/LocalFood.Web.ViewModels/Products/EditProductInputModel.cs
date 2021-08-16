namespace LocalFood.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EditProductInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Име на продукт")]
        [MinLength(3, ErrorMessage = "Името трябва да съдържа поне 3 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Цена")]
        [Range(0, 10000, ErrorMessage = "Цената трябва да бъде между 0 и 10 000 лв.")]
        public decimal Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето не може да бъде празно.")]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        public ICollection<CategoryInputModel> Categories { get; set; }
    }
}
