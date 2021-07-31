namespace LocalFood.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Regions = new HashSet<Region>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
