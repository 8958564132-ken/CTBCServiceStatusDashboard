using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTBCServiceStatusDashboard.Pages.UserAdmin
{
    [Authorize(Roles = "User Admin")]
    public class RoleManagementModel : PageModel
    {
        private readonly ILogger<RoleManagementModel> _logger;

        public RoleManagementModel(ILogger<RoleManagementModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Load role data
        }
    }
}