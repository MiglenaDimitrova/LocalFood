namespace LocalFood.Data.Models
{
    using LocalFood.Data.Common.Models;

    public class UserProducer : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }
    }
}
