using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Discription { get; set; }
        public int EmployeeID { get; set; }
        [NotMapped]
        public bool Remember { get; set; }
    }
}
