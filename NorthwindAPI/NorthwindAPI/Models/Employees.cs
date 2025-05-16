using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Models
{
    public class Employees
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public System.DateTime? BirthDate { get; set; }
        public System.DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        [ForeignKey("Employees")]
        public int? ReportsTo { get; set; }
    }
}
