namespace LocalFood.Data.Models
{
    using LocalFood.Data.Common.Models;

    public class MapLocation : BaseDeletableModel<int>
    {
        public int MapId { get; set; }

        public virtual Map Map { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
