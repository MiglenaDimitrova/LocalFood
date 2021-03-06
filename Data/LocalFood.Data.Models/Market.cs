namespace LocalFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class Market : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string UrlLocation { get; set; }

        public string FacebookPage { get; set; }

        [Required]
        public string FullAddress { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
