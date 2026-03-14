using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTBCServiceStatusDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // This runs when the page loads
        }
    }
}