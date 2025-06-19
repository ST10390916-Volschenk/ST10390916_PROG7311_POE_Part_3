using ST10390916_PROG7311_POE.Data;
using System.ComponentModel.DataAnnotations;

namespace ST10390916_PROG7311_POE.Models
{

    public enum UserRole
    {
        Employee,
        Farmer,
        Customer
    }

    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public UserRole Role { get; set; }

    }

}
