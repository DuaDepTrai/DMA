using System.ComponentModel.DataAnnotations;

namespace NorthwindAPIClient.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        [Required(ErrorMessage = "Region Description is required!")]
        public string RegionDescription { get; set; }
    }
}
