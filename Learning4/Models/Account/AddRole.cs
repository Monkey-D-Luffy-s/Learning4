using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Account
{
    public class AddRole
    {
        [Required(ErrorMessage = "Role is Required")]
        [MinLength(4, ErrorMessage = "Role must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Role cannot exceed 20 characters")]
        public string RoleName { get; set; }
    }
}
