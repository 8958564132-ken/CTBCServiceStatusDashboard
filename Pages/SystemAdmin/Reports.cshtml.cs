using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CTBCServiceStatusDashboard.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CTBCServiceStatusDashboard.Pages.SystemAdmin
{
    [Authorize(Roles = "System Admin")]
    public class ReportsModel : PageModel
    {
        private readonly ILogger<ReportsModel> _logger;

        public ReportsModel(ILogger<ReportsModel> logger)
        {
            _logger = logger;
        }

        public List<Report> Reports { get; set; }
        
        [BindProperty]
        public ReportFilter Filter { get; set; } = new ReportFilter();
        
        public List<UserActivityReportData> UserActivityData { get; set; }
        public List<UserMaintenanceReportData> UserMaintenanceData { get; set; }
        public List<AccessRoleReportData> AccessRoleData { get; set; }
        public List<ActiveUsersReportData> ActiveUsersData { get; set; }
        public List<PasswordExceptionReportData> PasswordExceptionData { get; set; }
        
        public string CurrentReportId { get; set; }
        public string CurrentReportName { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        private List<Report> GetAvailableReports()
        {
            return new List<Report>
            {
                new Report
                {
                    Id = "user_activity",
                    Name = "User Activity Report",
                    Description = "Detailed log of user logins, logouts, and failed attempts",
                    Category = "Activity",
                    Icon = "fa-users",
                    LastGenerated = DateTime.Now.AddDays(-1),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "user_maintenance",
                    Name = "User Maintenance Report",
                    Description = "Changes made to user accounts (create, update, delete, suspend)",
                    Category = "Maintenance",
                    Icon = "fa-tools",
                    LastGenerated = DateTime.Now.AddDays(-2),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "access_role",
                    Name = "Access Role Report",
                    Description = "List of roles and their assigned permissions",
                    Category = "Security",
                    Icon = "fa-shield-alt",
                    LastGenerated = DateTime.Now.AddDays(-3),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "active_users",
                    Name = "Active Users Report",
                    Description = "Currently active users and their last login times",
                    Category = "Activity",
                    Icon = "fa-user-check",
                    LastGenerated = DateTime.Now.AddHours(-5),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "password_exceptions",
                    Name = "Password Exception Report",
                    Description = "Password retry attempts, resets, and lockouts",
                    Category = "Security",
                    Icon = "fa-key",
                    LastGenerated = DateTime.Now.AddDays(-1),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "audit_trail",
                    Name = "Audit Trail Report",
                    Description = "Complete audit trail of system changes",
                    Category = "Audit",
                    Icon = "fa-history",
                    LastGenerated = DateTime.Now.AddDays(-1),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "parameter_changes",
                    Name = "Parameter Changes Report",
                    Description = "Changes to system parameters and settings",
                    Category = "Audit",
                    Icon = "fa-sliders-h",
                    LastGenerated = null,
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                },
                new Report
                {
                    Id = "dormant_accounts",
                    Name = "Dormant Accounts Report",
                    Description = "Accounts inactive for 30+ days",
                    Category = "Maintenance",
                    Icon = "fa-user-clock",
                    LastGenerated = DateTime.Now.AddDays(-7),
                    AvailableFormats = new List<string> { "excel", "pdf", "csv" }
                }
            };
        }

        private List<UserActivityReportData> GetMockUserActivityData()
        {
            var data = new List<UserActivityReportData>();
            var users = new[] { "admin", "jsmith", "mjohnson", "awilson", "pbrown", "slee", "rjohnson" };
            var roles = new[] { "System Admin", "User Admin", "LGU", "User", "User", "LGU", "User" };
            var activities = new[] { "Login", "Logout", "Failed Login", "Password Change", "Profile Update" };
            
            for (int i = 1; i <= 50; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 30)).AddHours(-new Random().Next(0, 24));
                var userIndex = new Random().Next(0, users.Length);
                var activityIndex = new Random().Next(0, activities.Length);
                var isSuccess = activityIndex == 2 ? false : (new Random().Next(0, 10) > 2);
                
                data.Add(new UserActivityReportData
                {
                    Id = i,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = users[userIndex],
                    Role = roles[userIndex],
                    Activity = activities[activityIndex],
                    Status = isSuccess ? "success" : "failure",
                    IpAddress = $"192.168.1.{new Random().Next(100, 200)}",
                    Details = isSuccess 
                        ? $"{activities[activityIndex]} successful"
                        : $"{activities[activityIndex]} failed - invalid credentials"
                });
            }
            
            return data.OrderByDescending(d => d.Date).ThenByDescending(d => d.Time).ToList();
        }

        private List<UserMaintenanceReportData> GetMockUserMaintenanceData()
        {
            var data = new List<UserMaintenanceReportData>();
            var initiators = new[] { "admin", "jsmith" };
            var targets = new[] { "mjohnson", "awilson", "pbrown", "slee", "rjohnson", "jdoe", "msmith" };
            var actions = new[] { "Create", "Update", "Delete", "Suspend", "Activate", "Role Change", "Password Reset" };
            var fields = new[] { "Role", "Status", "Email", "Department", "Contact Number" };
            
            for (int i = 1; i <= 30; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 45)).AddHours(-new Random().Next(0, 24));
                var action = actions[new Random().Next(0, actions.Length)];
                var field = fields[new Random().Next(0, fields.Length)];
                var oldValue = field == "Role" ? "User" : 
                              field == "Status" ? "active" : 
                              field == "Email" ? "old@email.com" : 
                              field == "Department" ? "Sales" : "123-456-7890";
                var newValue = field == "Role" ? "LGU" : 
                              field == "Status" ? "inactive" : 
                              field == "Email" ? "new@email.com" : 
                              field == "Department" ? "Marketing" : "123-456-7891";
                
                data.Add(new UserMaintenanceReportData
                {
                    Id = i,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    Initiator = initiators[new Random().Next(0, initiators.Length)],
                    TargetUser = targets[new Random().Next(0, targets.Length)],
                    Action = action,
                    Fieldchanged = action == "Create" ? "All Fields" : field,
                    OldValue = action == "Create" ? "-" : oldValue,
                    NewValue = newValue,
                    IpAddress = "192.168.1.100"
                });
            }
            
            return data.OrderByDescending(d => d.Date).ThenByDescending(d => d.Time).ToList();
        }

        private List<AccessRoleReportData> GetMockAccessRoleData()
        {
            return new List<AccessRoleReportData>
            {
                new AccessRoleReportData
                {
                    Role = "System Administrator",
                    UserCount = 2,
                    PermissionCount = 20,
                    Permissions = new List<string> { "View Dashboard", "Manage URLs", "Manage Users", "Manage Roles", "View Audit Logs", "Generate Reports", "System Settings" },
                    LastModified = DateTime.Parse("2024-03-01"),
                    Status = "active",
                    CreatedBy = "system"
                },
                new AccessRoleReportData
                {
                    Role = "User Administrator",
                    UserCount = 3,
                    PermissionCount = 12,
                    Permissions = new List<string> { "View Dashboard", "Manage Users", "Reset Passwords", "View Reports", "LGU Management" },
                    LastModified = DateTime.Parse("2024-02-15"),
                    Status = "active",
                    CreatedBy = "admin"
                },
                new AccessRoleReportData
                {
                    Role = "LGU User",
                    UserCount = 8,
                    PermissionCount = 6,
                    Permissions = new List<string> { "View Dashboard", "LGU Dashboard", "Remittance Status", "Project Status" },
                    LastModified = DateTime.Parse("2024-02-10"),
                    Status = "active",
                    CreatedBy = "admin"
                },
                new AccessRoleReportData
                {
                    Role = "Regular User",
                    UserCount = 15,
                    PermissionCount = 4,
                    Permissions = new List<string> { "View Dashboard", "View Status", "View History" },
                    LastModified = DateTime.Parse("2024-01-20"),
                    Status = "active",
                    CreatedBy = "admin"
                },
                new AccessRoleReportData
                {
                    Role = "Auditor",
                    UserCount = 1,
                    PermissionCount = 8,
                    Permissions = new List<string> { "View Dashboard", "View Audit Logs", "View Reports", "View Users" },
                    LastModified = DateTime.Parse("2024-01-05"),
                    Status = "active",
                    CreatedBy = "admin"
                },
                new AccessRoleReportData
                {
                    Role = "Content Manager",
                    UserCount = 0,
                    PermissionCount = 5,
                    Permissions = new List<string> { "Manage Announcements", "Manage FAQs", "Landing Page Content" },
                    LastModified = DateTime.Parse("2024-01-08"),
                    Status = "inactive",
                    CreatedBy = "admin"
                }
            };
        }

        private List<ActiveUsersReportData> GetMockActiveUsersData()
        {
            var data = new List<ActiveUsersReportData>();
            var users = new[] { "admin", "jsmith", "pbrown", "slee", "jdoe", "msmith" };
            var roles = new[] { "System Admin", "User Admin", "User", "LGU", "LGU", "User" };
            
            for (int i = 0; i < users.Length; i++)
            {
                var lastLogin = DateTime.Now.AddMinutes(-new Random().Next(5, 120));
                data.Add(new ActiveUsersReportData
                {
                    User = users[i],
                    Role = roles[i],
                    LastLogin = lastLogin,
                    LastLoginIP = $"192.168.1.{new Random().Next(100, 150)}",
                    SessionDuration = new Random().Next(5, 60),
                    Status = "active"
                });
            }
            
            return data.OrderByDescending(d => d.LastLogin).ToList();
        }

        private List<PasswordExceptionReportData> GetMockPasswordExceptionData()
        {
            var data = new List<PasswordExceptionReportData>();
            var users = new[] { "mjohnson", "awilson", "rjohnson", "bwilliams", "kchen" };
            var types = new[] { "failed_attempt", "lockout", "reset", "expiry" };
            var resolutions = new[] { "User unlocked", "Password reset", "Account reactivated", "Password changed" };
            
            for (int i = 1; i <= 15; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 30)).AddHours(-new Random().Next(0, 24));
                var typeIndex = new Random().Next(0, types.Length);
                
                data.Add(new PasswordExceptionReportData
                {
                    Id = i,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = users[new Random().Next(0, users.Length)],
                    ExceptionType = types[typeIndex],
                    Attempts = typeIndex == 0 ? new Random().Next(1, 4) : 
                              typeIndex == 1 ? 3 : 0,
                    IpAddress = $"192.168.1.{new Random().Next(150, 200)}",
                    Resolution = typeIndex == 1 ? resolutions[0] :
                                typeIndex == 2 ? resolutions[1] :
                                typeIndex == 3 ? resolutions[3] : "None"
                });
            }
            
            return data.OrderByDescending(d => d.Date).ThenByDescending(d => d.Time).ToList();
        }

        public void OnGet()
        {
            Reports = GetAvailableReports();
            
            // Initialize filter with default values
            Filter = new ReportFilter
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now,
                Format = "excel"
            };
            
            // Load mock data for previews
            UserActivityData = GetMockUserActivityData();
            UserMaintenanceData = GetMockUserMaintenanceData();
            AccessRoleData = GetMockAccessRoleData();
            ActiveUsersData = GetMockActiveUsersData();
            PasswordExceptionData = GetMockPasswordExceptionData();
        }

        public IActionResult OnPostGenerate(string reportId)
        {
            try
            {
                CurrentReportId = reportId;
                var report = GetAvailableReports().FirstOrDefault(r => r.Id == reportId);
                
                if (report != null)
                {
                    CurrentReportName = report.Name;
                    SuccessMessage = $"Report '{report.Name}' generated successfully. Download started.";
                    
                    // In production, this would generate the actual file
                    // For now, just show success message
                }
                
                // Reload data
                Reports = GetAvailableReports();
                UserActivityData = GetMockUserActivityData();
                UserMaintenanceData = GetMockUserMaintenanceData();
                AccessRoleData = GetMockAccessRoleData();
                ActiveUsersData = GetMockActiveUsersData();
                PasswordExceptionData = GetMockPasswordExceptionData();
                
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating report");
                ErrorMessage = "An error occurred while generating the report";
                return Page();
            }
        }

        public IActionResult OnPostSchedule(string reportId)
        {
            try
            {
                SuccessMessage = "Report scheduled successfully. You will receive it via email.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error scheduling report");
                ErrorMessage = "An error occurred while scheduling the report";
                return RedirectToPage();
            }
        }
    }
}