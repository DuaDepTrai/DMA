using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class EmployeeTerritories
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Employees")]
        public int EmployeeID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Territories")]
        public string TerritoryID { get; set; }
        public virtual Employees? Employees { get; set; }
        public virtual Territories? Territories { get; set; }
    }
}
