using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning4.Models.Leaves
{
    public class LeavesMaster
    {
        [Key]
        public string LeaveId { get; set; }
        public string? EmployeeId { get; set; }
        [Required(ErrorMessage = "LeaveType Required")]
        public int LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveTypeMaster? LeaveTypeMaster { get; set; }
        [Required(ErrorMessage = "Duration Required")]

        public int LeaveDays { get; set; }
        [Required(ErrorMessage = "LeaveFrom Required")]
        public DateTime LeaveFrom { get; set; }
        [Required(ErrorMessage = "LeaveTo Required")]
        public DateTime LeaveTo { get; set; }
        [Required(ErrorMessage = "Reason Required")]
        public string Reason { get; set; }
        public string? Remarks { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public StatusMaster? StatusMaster { get; set; }
    }
}
