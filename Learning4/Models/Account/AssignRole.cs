using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learning4.Models.Account
{
    public class AssignRole
    {
        public List<SelectListItem>? UserName { get; set; }
        public List<SelectListItem>? RoleName { get; set; }
        public string? SelectedUserName { get; set; }
        public string? SelectedRoleName { get; set; }
    }

  
}
