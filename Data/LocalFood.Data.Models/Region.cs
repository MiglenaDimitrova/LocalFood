namespace LocalFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Region : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
