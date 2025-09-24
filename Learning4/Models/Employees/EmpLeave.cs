using System.ComponentModel.DataAnnotations;

namespace Learning4.Models.Employees
{
    public class EmpLeave
    {
        [Key]
        public string LeaveId { get; set; }
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalDays { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; } // e.g., Pending, Approved, Rejected
    }
}
