namespace LocalFood.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class FoodLoss : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        public int? Quantity { get; set; }

        public DateTime? TimeRemaining { get; set; }
    }
}
