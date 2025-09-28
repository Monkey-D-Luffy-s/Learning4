using Learning4.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Learning4.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public  AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> AddUser(string username, string email, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return "Username, email, and password cannot be empty.";
            }
            else
            {

                var user = await _userManager.FindByNameAsync(username);
                if(user != null)
                {
                    return "User already exists.";
                }
                else
                {
                    var newUser = new IdentityUser
                    {
                        UserName = username,
                        Email = email,
                    };
                    var result = await _userManager.CreateAsync(newUser, password);
                    if(result.Succeeded)
                    {
                        return "User created successfully.";
                    }
                    else
                    {
                        return "Error creating user: " + string.Join(", ", result.Errors.Select(e => e.Description));
                    }
                }
            }
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return new LoginResponse{ Message = "User does not exist.",Data=null,IsSuccess=false };
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if(result.Succeeded)
                {
                    var Profile = await _userManager.FindByNameAsync(username);

                    var roles = await _userManager.GetRolesAsync(Profile);
                    string roleList = string.Join("& ", roles);
                    _httpContextAccessor.HttpContext.Session.SetString("UserName", Profile.UserName);
                    _httpContextAccessor.HttpContext.Session.SetString("Role",roleList);
                    return new LoginResponse { Message = "Login successful.", Data = Profile, IsSuccess = true };
                }
                else
                {
                    return new LoginResponse { Message = "Invalid username or password.", Data = null, IsSuccess = false };
                }
            }
                
        }

        public async Task<string> AddRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return "Role '" + roleName +"' created successfully!";
                }
                else
                {
                    return "Error: "+string.Join(", ", result.Errors.Select(e => e.Description));
                }
            }
            return "Role "+roleName+" already exists.";
        }

        public async Task<string> AssignRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return "User not found.";

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return "Role '"+roleName+"' assigned to "+user.UserName+"";
            }

            return "Error: "+string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<AssignRole> GetAllRoles()
        {
            AssignRole obj = new AssignRole();
            var roles = await _roleManager.Roles.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            obj.RoleName = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            obj.UserName = users.Select(r => new SelectListItem { Value = r.UserName, Text = r.UserName }).ToList();
            return obj;
        }

        public async Task Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
        }

        public IEnumerable<SelectListItem> GetRoles()
        {
            
            return _roleManager.Roles
                .Select(d => new SelectListItem
                {
                    Value = d.Name,
                    Text = d.Name
                })
                .ToList();
        }


    }
}
