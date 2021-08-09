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

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        [Required]
        public string LocalityName { get; set; }

        [Required]
        public string Adress { get; set; }

        public string UrlLocation { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public virtual ICollection<Map> Maps { get; set; }
    }
}
