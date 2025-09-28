using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Leaves
{
    public class LeaveTypeMaster
    {
        [Key]
        public int LeaveTypeId { get; set; }
        [Required(ErrorMessage = "LeaveType Required")]
        public string LeaveType { get; set; }
    }
}
