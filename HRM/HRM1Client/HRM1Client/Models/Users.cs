using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM1API.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Discription { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
