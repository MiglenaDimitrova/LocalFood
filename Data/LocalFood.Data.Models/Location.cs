namespace LocalFood.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        public Location()
        {
            this.Maps = new HashSet<Map>();
        }

        [Required]
        public string CountryName { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string TownName { get; set; }

        [Required]
        public string Adress { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public virtual ICollection<Map> Maps { get; set; }
    }
}
