namespace LocalFood.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Producer : BaseDeletableModel<int>
    {
        public Producer()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Site { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsBio { get; set; } = false;

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<FoodLoss> FoodLosses { get; set; }
    }
}
