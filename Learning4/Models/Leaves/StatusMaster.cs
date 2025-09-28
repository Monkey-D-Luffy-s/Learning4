using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Leaves
{
    public class StatusMaster
    {
        [Key]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Status Required")]
        public string Status { get; set; }
    }
}
