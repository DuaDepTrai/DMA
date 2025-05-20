using System.ComponentModel.DataAnnotations;

namespace NorthwindAPIClient.Models
{
    public class Territories
    {
        [Required(ErrorMessage = "Territory ID is required")]
        public string TerritoryID { get; set; }
        [Required(ErrorMessage = "Territory Description is required")]
        public string TerritoryDescription { get; set; }
        [Required(ErrorMessage = "Region is required")]
        public int? RegionID { get; set; }
        public Region? Region { get; set; }
    }
}
