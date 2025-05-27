using System.ComponentModel.DataAnnotations;

namespace NewsAPI4Client.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        [StringLength(50)]
        public string CategoryType { get; set; }
        public int? OrderInt { get; set; }
    }
}
