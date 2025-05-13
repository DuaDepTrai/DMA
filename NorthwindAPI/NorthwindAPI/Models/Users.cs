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
        [ForeignKey("Employees")]
        public int EmployeeID { get; set; }
        public virtual Employees? Employees { get; set; }
    }
}
