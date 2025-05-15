using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class Shippers
    {
        [Key]
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string? Phone {  get; set; }
    }
}
