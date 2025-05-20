using System.ComponentModel.DataAnnotations;

namespace NorthwindAPIClient.Models
{
    public class Users
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName is required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        public string Discription { get; set; }
        public int EmployeeID { get; set; }
        public bool Remember { get; set; }
        //public string? FullName { get; set; }
    }
}
