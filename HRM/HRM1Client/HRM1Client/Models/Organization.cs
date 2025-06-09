using System.ComponentModel.DataAnnotations;

namespace HRM1API.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationID { get; set; }
        public string Code { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
