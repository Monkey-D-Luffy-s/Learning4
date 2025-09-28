using Learning4.Models.Employees;
using Learning4.Services.Account;
using Learning4.Services.Coupons;
using Microsoft.AspNetCore.Mvc;

namespace Learning4.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly IAccountService _acountService;

        public EmployeeController(ICouponService couponService, IAccountService acountService)
        {
            _couponService = couponService;
            _acountService = acountService;
        }

        public IActionResult Index()
        {
            ViewBag.Districts = _couponService.GetDistrictsList();
            ViewBag.Colleges = _couponService.GetCollegesList();
            ViewBag.Roles = _acountService.GetRoles();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AddEmployeeModel emp)
        {
            if(ModelState.IsValid)
            {
                // Process the valid employee data (e.g., save to database)
                if (emp.Passport_Document != null && emp.Singnature_Document != null)
                {
                    // choose folder inside wwwroot
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // create folder if not exists
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + emp.Passport_Document.FileName;
                    var uniquesignFileName = Guid.NewGuid().ToString() + "_" + emp.Singnature_Document.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    var singPath = Path.Combine(uploadsFolder, uniquesignFileName);

                    // save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await emp.Passport_Document.CopyToAsync(stream);
                    }
                    using (var stream = new FileStream(singPath, FileMode.Create))
                    {
                        await emp.Singnature_Document.CopyToAsync(stream);
                    }

                    // store relative path in DB
                    emp.Passport_DocumentPath = "/uploads/" + uniqueFileName;
                    emp.Singnature_DocumentPath = "/uploads/" + uniquesignFileName;
                    try
                    {
                        var result = await _couponService.AddEmployeeAsync(emp);
                        if (result.ToString() == "Employee added successfully")
                        {
                            //HttpContext.Session.GetString("UserName");
                            //HttpContext.Session.GetString("Role");
                            TempData["SuccessMessage"] = result.ToString();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = result.ToString();
                            return RedirectToAction("Index");
                        }
                            
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = ex.Message;
                        return RedirectToAction("Index");

                    }
                    
                }
            }
            else
            {

                TempData["ErrorMessage"] = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();
                ViewBag.Message = "There are validation errors.";
            }
            return View();
        }



    }
}
