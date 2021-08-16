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

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
