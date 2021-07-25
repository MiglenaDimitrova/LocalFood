namespace LocalFood.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LocalFood.Data.Common.Models;

    public class Map : BaseDeletableModel<int>
    {
        public Map()
        {
            this.Locations = new HashSet<Location>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
