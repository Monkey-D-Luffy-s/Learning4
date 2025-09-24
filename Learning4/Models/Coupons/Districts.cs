using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learning4.Models.Coupons
{
    public class Districts
    {
        [Key]
        public int DistrictID { get; set; }
        [Required(ErrorMessage = "DistrictName is Required")]
        [MinLength(4, ErrorMessage = "DistrictName must be at least 4 characters long.")]
        [MaxLength(50, ErrorMessage = "DistrictName cannot exceed 50 characters.")]
        public string DistrictName { get; set; }
    }
}
