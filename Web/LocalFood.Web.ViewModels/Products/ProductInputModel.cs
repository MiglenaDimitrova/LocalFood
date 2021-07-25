namespace LocalFood.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    public class ProductInputModel
    {
        [Required]
        [Display(Name="Име")]
        [MinLength(3, ErrorMessage ="Името трябва да съдържа поне 3 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Въведи цена.")]
        [Display(Name = "Цена")]
        [Range(0, 10000, ErrorMessage ="Цената трябва да бъде между 0 и 10 000 лв.")]
        public decimal Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Снимка")]
        public string Image { get; set; }

        [Required(ErrorMessage ="Въведи категория.")]
        [Display(Name = "Категория")]
        public string Category { get; set; }
    }
}
