using Learning4.Models.Coupons;
using Learning4.Models.Leaves;
using Learning4.Services.Coupons;
using Learning4.Services.Leaves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learning4.Controllers
{
    public class mLeavesController : Controller
    {
        private readonly ILeaveService _leavesService;
        private readonly ICouponService _couponService;
        public mLeavesController(ILeaveService leavesService, ICouponService couponService)
        {
            _leavesService = leavesService;
            _couponService = couponService;
        }

        public IActionResult Index()
        {
            ViewBag.LeaveTypes = _leavesService.GetLeaveTypesList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LeavesMaster leaves)
        {
            var userid = HttpContext.Session.GetString("UserName");
            try
            {
                // Fix: Use User.Identity.Name to get the logged-in user's name
                leaves.EmployeeId = userid ?? User.Identity?.Name;
                leaves.StatusId = 1; // Pending
                var res = await _leavesService.ApplyLeave(leaves);
                if (res.Contains("Successfully"))
                {
                    TempData["SuccessMessage"] = res;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = res;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetAllLeave()
        {
            var userid = HttpContext.Session.GetString("UserName") ?? User.Identity.Name;
            var districts = await _leavesService.GetAllEmployeeLeaves(userid);
            return Json(districts);
        }


        public async Task<IActionResult> AddStatusType()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStatusType(StatusMaster statusModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _leavesService.AddStatus(statusModel);
                if (result == "Status Added Successfully")
                {
                    ViewBag.Message = result;
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("AddStatusType");
                }
                else
                {
                    ViewBag.Message = "Error: " + result;
                    TempData["ErrorMessage"] = "Error: " + result;
                    ModelState.AddModelError("Status", result);
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Error: Invalid Status data.";
                TempData["ErrorMessage"] = "Error: Invalid Status data.";
                return View();
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetAllStatusTypes()
        {
            var districts = await _leavesService.GetAllStatusTypes();
            return Json(districts);
        }

        public async Task<IActionResult> AddLeaveType()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddLeaveType(LeaveTypeMaster status)
        {
            if (ModelState.IsValid)
            {
                var result = await _leavesService.AddLeaveType(status);
                if (result == "Leave Type Added Successfully")
                {
                    ViewBag.Message = result;
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("AddLeaveType");
                }
                else
                {
                    ViewBag.Message = "Error: " + result;
                    TempData["ErrorMessage"] = "Error: " + result;
                    ModelState.AddModelError("Status", result);
                    return RedirectToAction("AddLeaveType");
                }
            }
            else
            {
                ViewBag.Message = "Error: Invalid leave type data.";
                TempData["ErrorMessage"] = "Error: Invalid leave type data.";
                return RedirectToAction("AddLeaveType");
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeaveTypes()
        {
            var districts = await _leavesService.GetAllLeaveTypes();
            return Json(districts);
        }
        [Authorize(Roles = "Principal")]
        public async Task<IActionResult> PrincipalViewLeaves()
        {
            ViewBag.Employees = _couponService.GetEmployeeList();
            List<LeavesMaster> leaves = await _leavesService.GetAllEmployeesLeavesforPrincipal(User.Identity?.Name);
            return View(leaves);
        }
        [HttpPost]
        public async Task<JsonResult> GetAllLeavesByEmployeeID(string EmployeeId)
        {
            var districts = await _leavesService.GetAllEmployeeLeaves(EmployeeId);
            return Json(districts);
        }
        [Authorize(Roles = "Principal")]
        [HttpPost]
        public async Task<IActionResult> ApproveLeave(string id)
        {
            var leaveDetails = await _leavesService.GetLeaveDetails(id);
            if (leaveDetails != null)
            {
                var result = await _leavesService.ApproveorRejectLeave(leaveDetails, 4);
                if (result.Contains("successfully"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("PrincipalViewLeaves");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("PrincipalViewLeaves");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Leave not found.";
                return RedirectToAction("PrincipalViewLeaves");
            }
        }
        [Authorize(Roles = "Principal")]
        [HttpPost]
        public async Task<IActionResult> RejectLeave(string id)
        {
            var leaveDetails = await _leavesService.GetLeaveDetails(id);
            if (leaveDetails != null)
            {
                var result = await _leavesService.ApproveorRejectLeave(leaveDetails, 3);
                if (result.Contains("successfully"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("PrincipalViewLeaves");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("PrincipalViewLeaves");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Leave not found.";
                return RedirectToAction("PrincipalViewLeaves");
            }
        }
        [HttpPost]
        public async Task <IActionResult> CancelLeave(string id)
        {
            var leaveDetails = await _leavesService.GetLeaveDetails(id);
            if (leaveDetails != null)
            {
                var result = await _leavesService.CancelLeave(leaveDetails);
                if (result.Contains("successfully"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Leave not found.";
                return RedirectToAction("Index");
            }
        }
    }
}
