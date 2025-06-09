using System.ComponentModel.DataAnnotations;

namespace HRM1API.Models
{
    public class Sex
    {
        [Key]
        public int SexID { get; set; }
        public string SexName { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
