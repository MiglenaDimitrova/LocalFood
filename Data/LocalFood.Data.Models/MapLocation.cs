namespace LocalFood.Data.Models
{
    using LocalFood.Data.Common.Models;

    public class MapLocation : BaseDeletableModel<int>
    {
        public int MapId { get; set; }

        public Map Map { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
