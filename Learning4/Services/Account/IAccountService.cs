using Learning4.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learning4.Services.Account
{
    public interface IAccountService
    {
        Task<string> AddUser(string username,string email, string password);

        Task<LoginResponse> Login(string username, string password);

        Task<string> AddRole(string role);

        Task<string> AssignRole(string roleName, string userName);
        Task<AssignRole> GetAllRoles();

        Task Logout();

        IEnumerable<SelectListItem> GetRoles();


    }
}
