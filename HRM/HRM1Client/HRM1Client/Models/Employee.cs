using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM1API.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateofBirth { get; set; }
        [ForeignKey("Sex")]
        public int SexID { get; set; }
        [ForeignKey("Organization")]
        public int OrganizationID { get; set; }
        [ForeignKey("Position")]
        public int PositionID { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual Position? Position { get; set; }
        public virtual Sex? Sex { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
