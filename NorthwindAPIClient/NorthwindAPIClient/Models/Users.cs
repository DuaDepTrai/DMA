using System.ComponentModel.DataAnnotations;

namespace NorthwindAPIClient.Models
{
    public class Users
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName is required!")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string? Password { get; set; }
        [Compare("Password")]
        public string? RePassword { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Employee is required!")]
        public int? EmployeeID { get; set; }
        public bool Remember { get; set; }
        //public string? FullName { get; set; }
    }
}
