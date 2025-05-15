using System.ComponentModel.DataAnnotations;

namespace NorthwindAPIClient.Models
{
    public class Shippers
    {
        public int ShipperID { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [Phone]
        public string? Phone { get; set; }
    }
}
