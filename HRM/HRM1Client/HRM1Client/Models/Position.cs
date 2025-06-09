using System.ComponentModel.DataAnnotations;

namespace HRM1API.Models
{
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }

    }
}
