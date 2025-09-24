using Learning4.Filters;
using Learning4.Models.Coupons;
using Learning4.Services.Coupons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning4.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AddCouponModel coupon)
        {
            if (ModelState.IsValid)
            {
                // Logic to save the coupon to the database would go here
                // For example: _context.Coupons.Add(coupon); _context.SaveChanges();
                var result = await _couponService.AddCoupon(coupon);
                if(result == "Coupon added successfully!")
                {
                    ViewBag.Message = result;
                    return RedirectToAction("CouponList");
                }
                else
                {
                    ViewBag.Message = "Error: " + result;
                    ModelState.AddModelError("Disccount", result);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "Error: Invalid coupon data.";

                return RedirectToAction("Index");
            }
        }

        [ServiceFilter(typeof(CouponCatchFilter))]
        public async Task<IActionResult> CouponList()
        {
            var list = await _couponService.GetAllCoupons();
            return View(list);
        }

        public async Task<IActionResult> AddDistrict()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDistrict(Districts dist)
        {
           if(ModelState.IsValid)
            {
                var result = await _couponService.AddDistrict(dist);
                if(result == "District added successfully!")
                {
                    ViewBag.Message = result;
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("AddDistrict"); 
                }
                else
                {
                    ViewBag.Message = "Error: " + result;
                    TempData["ErrorMessage"] = "Error: " + result;
                    ModelState.AddModelError("DistrictName", result);
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Error: Invalid district data.";
                TempData["ErrorMessage"] = "Error: Invalid district data.";
                return View();
            }
        }
        [HttpGet]
        public JsonResult GetDistricts()
        {
            var districts = _couponService.GetAllDistricts().Result;
            return Json(districts);
        }
        public async Task<IActionResult> AddColleges()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddColleges(Colleges dist)
        {
            if (ModelState.IsValid)
            {
                var result = await _couponService.AddCollege(dist);
                if (result == "College added successfully!")
                {
                    ViewBag.Message = result;
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("AddColleges");
                }
                else
                {
                    ViewBag.Message = "Error: " + result;
                    TempData["ErrorMessage"] = "Error: " + result;
                    ModelState.AddModelError("DistrictName", result);
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Error: Invalid district data.";
                TempData["ErrorMessage"] = "Error: Invalid district data.";
                return View();
            }
        }
        [HttpGet]
        public JsonResult GetColleges()
        {
            var districts = _couponService.GetAllColleges().Result;
            return Json(districts);
        }

     
    }
}
