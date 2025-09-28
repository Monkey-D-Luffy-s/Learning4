using Learning4.data;
using Learning4.Models.Leaves;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning4.Services.Leaves
{
    public class LeaveService : ILeaveService
    {
        private readonly LeavesDbContext _context;
        public LeaveService(LeavesDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddLeaveType(LeaveTypeMaster leavetype)
        {
            try
            {
                await _context.LeaveTypeMasters.AddAsync(leavetype);
                await _context.SaveChangesAsync();
                return "Leave Type Added Successfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public IEnumerable<SelectListItem> GetLeaveTypesList()
        {
            return _context.LeaveTypeMasters
                .Select(d => new SelectListItem
                {
                    Value = d.LeaveTypeId.ToString(),
                    Text = d.LeaveType
                })
                .ToList();
        }
        public async Task<List<LeaveTypeMaster>> GetAllLeaveTypes()
        {
            try
            {
                return await _context.LeaveTypeMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<string> AddStatus(StatusMaster leavetype)
        {
            try
            {
                await _context.StatusMasters.AddAsync(leavetype);
                await _context.SaveChangesAsync();
                return "Status Added Successfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public IEnumerable<SelectListItem> GetStatusList()
        {
            return _context.StatusMasters
                .Select(d => new SelectListItem
                {
                    Value = d.StatusId.ToString(),
                    Text = d.Status
                })
                .ToList();
        }
        public async Task<List<StatusMaster>> GetAllStatusTypes()
        {
            try
            {
                return await _context.StatusMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ApplyLeave(LeavesMaster Leave)
        {
            
            try
            {
                var count = await _context.LeavesMasters.CountAsync();
                var leavId = count < 1 ? "L00001" : $"L{(count + 1).ToString("D4")}";
                Leave.LeaveId = Convert.ToString(leavId);
                Leave.Remarks = "";
                await _context.LeavesMasters.AddAsync(Leave);
                await _context.SaveChangesAsync();
                return "Leave Applied Successfully.";
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            
        }

        public async Task<List<LeavesMaster>> GetAllLeaves()
        {
            try
            {
                return await _context.LeavesMasters
                    .Include(l => l.LeaveTypeMaster)  // Include LeaveType
                    .Include(l => l.StatusMaster)     // Include Status
                    .ToListAsync(); ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<LeavesMaster>> GetAllEmployeeLeaves(string empId)
        {
            try
            {
                return await _context.LeavesMasters
                    .Include(l => l.LeaveTypeMaster)
                    .Include(l => l.StatusMaster)
                    .Where(l => l.EmployeeId == empId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<LeavesMaster>> GetAllEmployeesLeavesforPrincipal(string empId)
        {
            try
            {
                return await _context.LeavesMasters
                    .Include(l => l.LeaveTypeMaster)
                    .Include(l => l.StatusMaster)
                    .Where(l => l.EmployeeId != empId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<LeavesMaster?> GetLeaveDetails(string leaveId)
        {
            try
            {
                return await _context.LeavesMasters.FirstOrDefaultAsync(l => l.LeaveId == leaveId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdateLeave(LeavesMaster leave)
        {
            try
            {
                var existingLeave = await _context.LeavesMasters.FindAsync(leave.LeaveId);
                if (existingLeave == null)
                {
                    return "Leave not found.";
                }
                // Update fields
                existingLeave.LeaveFrom = leave.LeaveFrom;
                existingLeave.LeaveTo = leave.LeaveTo;
                existingLeave.LeaveTypeId = leave.LeaveTypeId;
                existingLeave.StatusId = leave.StatusId;
                existingLeave.Remarks = leave.Remarks;
                _context.LeavesMasters.Update(existingLeave);
                await _context.SaveChangesAsync();
                return "Leave updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> CancelLeave(LeavesMaster leave)
        {
            try
            {
                var existingLeave = await _context.LeavesMasters.FindAsync(leave.LeaveId);
                if (existingLeave == null)
                {
                    return "Leave not found.";
                }
                existingLeave.StatusId = 5;
                _context.LeavesMasters.Update(existingLeave);
                await _context.SaveChangesAsync();
                return "Leave Cancelled successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public async Task<string> ApproveorRejectLeave(LeavesMaster leave,int id)
        {
            try
            {
                var existingLeave = await _context.LeavesMasters.FindAsync(leave.LeaveId);
                if (existingLeave == null)
                {
                    return "Leave not found.";
                }
                existingLeave.StatusId = id;
                existingLeave.Remarks = leave.Remarks;
                _context.LeavesMasters.Update(existingLeave);
                await _context.SaveChangesAsync();
                return "Leave Cancelled successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
