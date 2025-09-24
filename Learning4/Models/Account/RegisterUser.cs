using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Account
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
