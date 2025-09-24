using Learning4.data;
using Learning4.Models.Coupons;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning4.Services.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly IDbContextFactory<CouponDbContext> _couponFactory;
        public CouponService(IDbContextFactory<CouponDbContext> couponFactory)
        {
            _couponFactory = couponFactory;
        }

        public async Task<string> AddEmployeeAsync(Models.Employees.AddEmployeeModel emp)
        {
            using var db = _couponFactory.CreateDbContext();

                var lastEmployee = db.Employees
                    .OrderByDescending(e => e.CreatedAt)
                    .FirstOrDefault();
            string nextId = (lastEmployee == null ? 11000 : Convert.ToInt32(lastEmployee.EmployeeId) + 1).ToString();
            emp.EmployeeId = nextId;
            Models.Employees.Employee employee = new Models.Employees.Employee
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                RoleId = emp.RoleId,
                Salary = emp.Salary,
                DateOfJoining = emp.DateOfJoining,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Address = emp.Address,
                CollegeId = emp.CollegeId,
                DistrictID = emp.DistrictID,
                AdhaarNumber = emp.AdhaarNumber,
                PANNumber = emp.PANNumber,
                Passport_DocumentPath = emp.Passport_DocumentPath,
                BloodGroup = emp.BloodGroup,
                Singnature_DocumentPath = emp.Singnature_DocumentPath,
                IsActive = "Y",
                CreatedAt = DateTime.Now
            };
            try
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return "Employee added successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> AddCoupon(AddCouponModel coupon)
        {
            using var db = _couponFactory.CreateDbContext();
            CoouponBase newCoupon = new CoouponBase
            {
                CouponId = Guid.NewGuid(),
                CouponCode = coupon.CouponCode,
                DiscountAmount = coupon.DiscountAmount,
                MinimumPurchaseAmount = coupon.MinimumPurchaseAmount,
                ExpiryDate = coupon.ExpiryDate
            };
            try
            {
                await db.Coupons.AddAsync(newCoupon);
                await db.SaveChangesAsync();
                return "Coupon added successfully!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<CoouponBase>> GetAllCoupons()
        {
            using var db = _couponFactory.CreateDbContext();
            return await db.Coupons.ToListAsync();
        }


        public async Task<string> AddDistrict(Districts dist)
        {
            using var db = _couponFactory.CreateDbContext();
            try
            {
                await db.Districts.AddAsync(dist);
                await db.SaveChangesAsync();
                return "District added successfully!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<Districts>> GetAllDistricts()
        {
            using var db = _couponFactory.CreateDbContext();
            return await db.Districts.ToListAsync();
        }

        public async Task<string> AddCollege(Colleges dist)
        {
            using var db = _couponFactory.CreateDbContext();
            try
            {
                await db.Colleges.AddAsync(dist);
                await db.SaveChangesAsync();
                return "College added successfully!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<Colleges>> GetAllColleges()
        {
            using var db = _couponFactory.CreateDbContext();
            return await db.Colleges.ToListAsync();
        }
        public IEnumerable<SelectListItem> GetDistrictsList()
        {
            using var db = _couponFactory.CreateDbContext();
            return db.Districts
                .Select(d => new SelectListItem
                {
                    Value = d.DistrictID.ToString(),
                    Text = d.DistrictName
                })
                .ToList();
        }
        public IEnumerable<SelectListItem> GetCollegesList()
        {
            using var db = _couponFactory.CreateDbContext();
            return db.Colleges
                .Select(d => new SelectListItem
                {
                    Value = d.CollegeID.ToString(),
                    Text = d.CollegeName
                })
                .ToList();
        }
    }
}
