namespace LocalFood.Web.ViewModels.Producers
{
    using System;

    public class ProducerViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string CompanyName { get; set; }

        public string Image { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Site { get; set; }

        public string Description { get; set; }

        public string FullAddress { get; set; }

        public string UrlLocation { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AverageVote { get; set; }
    }
}
