namespace LocalFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsBio { get; set; } = false;

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
