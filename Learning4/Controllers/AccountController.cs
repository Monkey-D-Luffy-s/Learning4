using Learning4.Models.Account;
using Learning4.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Learning4.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accounnt;
       
        public AccountController(IAccountService accounnt)
        {
            _accounnt = accounnt;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password and Confirm Password do not match");
                    return RedirectToAction("Index");
                }

                var result = await _accounnt.AddUser(user.UserName, user.Email, user.Password);
                if (result == "User created successfully.")
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    ModelState.AddModelError(string.Empty, result);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username) && string.IsNullOrEmpty(user.Password))
                {

                    ModelState.AddModelError(string.Empty, "Username and Password cannot be empty");
                    return View(user);
                }
                else
                {
                    var result = await _accounnt.Login(user.Username, user.Password);
                    if (result.IsSuccess)
                    {
                        TempData["SuccessMessage"] = "LoggedIn SuccessFully";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        TempData["ErrorMessage"] = result.Message;
                        ModelState.AddModelError(string.Empty, result.Message);
                        return View(user);
                    }
                }
            }
            else
            {
                return View(user);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }
       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(AddRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _accounnt.AddRole(role.RoleName);
                if (result.Contains("created successfully"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                //ViewBag.Message = $"Role '{role.RoleName}' has been assigned successfully.";
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssingRole()
        {
            AssignRole model = await _accounnt.GetAllRoles();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(AssignRole obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _accounnt.AssignRole(obj.SelectedUserName, obj.SelectedRoleName);
                if(result.Contains("assigned to"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                //ViewBag.Message = $"Role '{roleName}' has been assigned successfully.";
            }
            return RedirectToAction("AssingRole");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accounnt.Logout(); // clears the cookie
            return RedirectToAction("Login", "Account"); // redirect after logout
        }
    }
}
