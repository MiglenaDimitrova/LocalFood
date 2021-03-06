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
            this.FoodLosses = new HashSet<FoodLoss>();
            this.Votes = new HashSet<Vote>();
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
        [MinLength(10)]
        public string Description { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<FoodLoss> FoodLosses { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
