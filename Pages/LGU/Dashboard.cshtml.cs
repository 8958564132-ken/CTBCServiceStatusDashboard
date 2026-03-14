using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace CTBCServiceStatusDashboard.Pages.LGU
{
    [Authorize(Roles = "LGU, User Admin")]
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;

        public DashboardModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }

        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int CompletedProjects { get; set; }
        public decimal TotalRemittances { get; set; }
        public decimal MonthlyRemittance { get; set; }
        public List<RecentActivity> RecentActivities { get; set; }

        public class RecentActivity
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Time { get; set; }
            public string Icon { get; set; }
            public string Color { get; set; }
        }

        public void OnGet()
        {
            // Mock data for LGU dashboard
            TotalProjects = 8;
            ActiveProjects = 5;
            CompletedProjects = 3;
            TotalRemittances = 1245000;
            MonthlyRemittance = 245000;

            RecentActivities = new List<RecentActivity>
            {
                new RecentActivity
                {
                    Title = "Project Update",
                    Description = "Road construction project is 75% complete",
                    Time = "2 hours ago",
                    Icon = "fa-road",
                    Color = "primary"
                },
                new RecentActivity
                {
                    Title = "Remittance Received",
                    Description = "Monthly remittance of ₱245,000 received",
                    Time = "1 day ago",
                    Icon = "fa-money-bill-wave",
                    Color = "success"
                },
                new RecentActivity
                {
                    Title = "Project Started",
                    Description = "New school building project initiated",
                    Time = "3 days ago",
                    Icon = "fa-school",
                    Color = "info"
                },
                new RecentActivity
                {
                    Title = "Report Generated",
                    Description = "Monthly progress report generated",
                    Time = "5 days ago",
                    Icon = "fa-file-alt",
                    Color = "warning"
                }
            };
        }
    }
}