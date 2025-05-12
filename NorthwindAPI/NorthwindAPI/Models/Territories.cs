using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class Territories
    {
        [Key]
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        public Region Region { get; set; }
    }
}
