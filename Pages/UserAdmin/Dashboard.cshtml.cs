using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace CTBCServiceStatusDashboard.Pages.UserAdmin
{
    [Authorize(Roles = "User Admin, LGU")]
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;

        public DashboardModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }

        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int PendingActions { get; set; }
        public List<ServiceStatus> Services { get; set; }
        public List<RecentActivity> RecentActivities { get; set; }

        public class ServiceStatus
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Server { get; set; }
            public string IpAddress { get; set; }
            public int Port { get; set; }
            public string UrlPath { get; set; }
            public string Status { get; set; }
            public string LastChecked { get; set; }
        }

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
            // Mock data for user statistics
            TotalUsers = 24;
            ActiveUsers = 18;
            PendingActions = 6;

            // Mock data for services
            Services = new List<ServiceStatus>
            {
                new ServiceStatus
                {
                    Id = 1,
                    Name = "Main API",
                    Server = "api-server-01",
                    IpAddress = "192.168.1.100",
                    Port = 443,
                    UrlPath = "/health",
                    Status = "Online",
                    LastChecked = "2 min ago"
                },
                new ServiceStatus
                {
                    Id = 2,
                    Name = "Database",
                    Server = "db-server-01",
                    IpAddress = "192.168.1.200",
                    Port = 3306,
                    UrlPath = "/status",
                    Status = "Online",
                    LastChecked = "3 min ago"
                },
                new ServiceStatus
                {
                    Id = 3,
                    Name = "Payment Gateway",
                    Server = "pay-server-01",
                    IpAddress = "192.168.1.30",
                    Port = 8443,
                    UrlPath = "/check",
                    Status = "Offline",
                    LastChecked = "5 min ago"
                },
                new ServiceStatus
                {
                    Id = 4,
                    Name = "Auth Service",
                    Server = "auth-server-01",
                    IpAddress = "192.168.1.40",
                    Port = 8080,
                    UrlPath = "/auth/health",
                    Status = "Online",
                    LastChecked = "4 min ago"
                },
                new ServiceStatus
                {
                    Id = 5,
                    Name = "Storage API",
                    Server = "store-server-01",
                    IpAddress = "192.168.1.50",
                    Port = 9000,
                    UrlPath = "/health",
                    Status = "Online",
                    LastChecked = "7 min ago"
                },
                new ServiceStatus
                {
                    Id = 6,
                    Name = "CDN",
                    Server = "cdn-server-01",
                    IpAddress = "192.168.1.60",
                    Port = 80,
                    UrlPath = "/status",
                    Status = "Online",
                    LastChecked = "10 min ago"
                }
            };

            // Mock data for recent activity
            RecentActivities = new List<RecentActivity>
            {
                new RecentActivity
                {
                    Title = "New user created",
                    Description = "John Doe • Regular User • Marketing",
                    Time = "5 min ago",
                    Icon = "fa-user-plus",
                    Color = "success"
                },
                new RecentActivity
                {
                    Title = "User updated",
                    Description = "Jane Smith • Role changed to LGU",
                    Time = "2 hours ago",
                    Icon = "fa-user-edit",
                    Color = "info"
                },
                new RecentActivity
                {
                    Title = "Password reset",
                    Description = "Mike Johnson • Reset email sent",
                    Time = "1 day ago",
                    Icon = "fa-key",
                    Color = "warning"
                },
                new RecentActivity
                {
                    Title = "Account unlocked",
                    Description = "Alice Wilson • User was locked",
                    Time = "2 days ago",
                    Icon = "fa-lock-open",
                    Color = "primary"
                }
            };
        }
    }
}