using Learning4.Models.Coupons;
using Learning4.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace Learning4.data
{
    public class CouponDbContext : DbContext
    {
        public CouponDbContext(DbContextOptions<CouponDbContext> options) : base(options)
        {
        }

       public DbSet<CoouponBase> Coupons { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Districts> Districts { get; set; }
        public DbSet<Colleges> Colleges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue("Y");
        }

    }
}
