namespace LocalFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Market : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
