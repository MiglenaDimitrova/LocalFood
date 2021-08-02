namespace LocalFood.Data.Models
{
    using LocalFood.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public byte Value { get; set; }
    }
}
