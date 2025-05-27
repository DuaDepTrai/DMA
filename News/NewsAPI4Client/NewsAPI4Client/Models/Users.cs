using System.ComponentModel.DataAnnotations;

namespace NewsAPI4Client.Models
{
    public class Users
    {
        public int UsersID { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(250)]
        public string Discription { get; set; }
    }
}
