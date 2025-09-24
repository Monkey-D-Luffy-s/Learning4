using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Account
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
