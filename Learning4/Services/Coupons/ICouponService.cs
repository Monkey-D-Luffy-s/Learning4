using Learning4.Models.Coupons;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learning4.Services.Coupons
{
    public interface ICouponService
    {
        Task<string> AddCoupon(AddCouponModel coupon);
        Task<List<CoouponBase>> GetAllCoupons();

        Task<string> AddEmployeeAsync(Models.Employees.AddEmployeeModel emp);

        Task<string> AddDistrict(Districts dist);

        Task<List<Districts>> GetAllDistricts();
        Task<string> AddCollege(Colleges dist);

        Task<List<Colleges>> GetAllColleges();

        IEnumerable<SelectListItem> GetDistrictsList();
        IEnumerable<SelectListItem> GetCollegesList();
    }
}
