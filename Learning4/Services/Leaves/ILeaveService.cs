using Learning4.Models.Leaves;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learning4.Services.Leaves
{
    public interface ILeaveService
    {
        Task<string> AddLeaveType(LeaveTypeMaster leavetype);
        IEnumerable<SelectListItem> GetLeaveTypesList();
        Task<string> AddStatus(StatusMaster leavetype);
        IEnumerable<SelectListItem> GetStatusList();
        Task<string> ApplyLeave(LeavesMaster Leave);
        Task<List<LeavesMaster>> GetAllLeaves();
        Task<List<LeavesMaster>> GetAllEmployeeLeaves(string empId);
        Task<LeavesMaster?> GetLeaveDetails(string leaveId);
        Task<List<StatusMaster>> GetAllStatusTypes();
        Task<List<LeaveTypeMaster>> GetAllLeaveTypes();
        Task<string> UpdateLeave(LeavesMaster leave);
        Task<string> CancelLeave(LeavesMaster leave);
        Task<string> ApproveorRejectLeave(LeavesMaster leave, int id);
        Task<List<LeavesMaster>> GetAllEmployeesLeavesforPrincipal(string empId);
    }
}
