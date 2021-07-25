namespace LocalFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LocalFood.Data.Common.Models;

    public class ContactForm : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
