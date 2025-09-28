using Microsoft.EntityFrameworkCore;

namespace Learning4.data
{
    public class LeavesDbContext : DbContext
    {
        public LeavesDbContext(DbContextOptions<LeavesDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Leaves.StatusMaster> StatusMasters { get; set; }
        public DbSet<Models.Leaves.LeaveTypeMaster> LeaveTypeMasters { get; set; }
        public DbSet<Models.Leaves.LeavesMaster> LeavesMasters {get; set;}
    }
}
