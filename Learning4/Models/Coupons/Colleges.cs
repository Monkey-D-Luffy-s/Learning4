using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Coupons
{
    public class Colleges
    {
        [Key]
        public int CollegeID { get; set; }
        [Required(ErrorMessage = "CollegeName is Required")]
        [MinLength(4, ErrorMessage = "CollegeName must be at least 4 characters long.")]
        [MaxLength(50, ErrorMessage = "CollegeName cannot exceed 50 characters.")]
        public string CollegeName { get; set; }
    }
}
