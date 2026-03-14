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
    public class AuditLogsModel : PageModel
    {
        private readonly ILogger<AuditLogsModel> _logger;

        public AuditLogsModel(ILogger<AuditLogsModel> logger)
        {
            _logger = logger;
        }

        public List<AuditLog> ActivityLogs { get; set; }
        public List<AuditLog> MaintenanceLogs { get; set; }
        public List<AuditLog> ParameterLogs { get; set; }
        public List<AuditLog> UrlLogs { get; set; }  // ADD THIS LINE
        
        public AuditLogSummary Summary { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string ActionFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string UserFilter { get; set; }
        
        public List<string> UniqueUsers { get; set; }
        public List<string> UniqueActions { get; set; }

        // Mock data - URL Management Logs
        private List<AuditLog> GetMockUrlLogs()
        {
            var logs = new List<AuditLog>();
            var users = new[] { "admin", "jsmith" };
            var actions = new[] { "Create", "Update", "Delete", "Toggle Status", "Test Ping" };
            var urlNames = new[] { "Main API", "Database", "Payment Gateway", "Auth Service", "Storage API", "CDN" };
            
            for (int i = 1; i <= 15; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 7)).AddHours(-new Random().Next(0, 24));
                var userIndex = new Random().Next(0, users.Length);
                var actionIndex = new Random().Next(0, actions.Length);
                var urlIndex = new Random().Next(0, urlNames.Length);
                var isSuccess = new Random().Next(0, 10) > 1; // 90% success rate
                
                string oldValue = null;
                string newValue = null;
                string details = "";
                
                if (actions[actionIndex] == "Create")
                {
                    newValue = $"{urlNames[urlIndex]}|api-server-01|192.168.1.{new Random().Next(10, 100)}|{new Random().Next(80, 8443)}|/health|active";
                    details = $"Created new URL: {urlNames[urlIndex]}";
                }
                else if (actions[actionIndex] == "Update")
                {
                    oldValue = $"{urlNames[urlIndex]}|api-server-01|192.168.1.{new Random().Next(10, 100)}|{new Random().Next(80, 8443)}|/health|active";
                    newValue = $"{urlNames[urlIndex]}|api-server-01|192.168.1.{new Random().Next(10, 100)}|{new Random().Next(80, 8443)}|/health|active";
                    details = $"Updated URL: {urlNames[urlIndex]} - Changed IP address";
                }
                else if (actions[actionIndex] == "Delete")
                {
                    oldValue = $"{urlNames[urlIndex]}|api-server-01|192.168.1.{new Random().Next(10, 100)}|{new Random().Next(80, 8443)}|/health|active";
                    details = $"Deleted URL: {urlNames[urlIndex]}";
                }
                else if (actions[actionIndex] == "Toggle Status")
                {
                    oldValue = "active";
                    newValue = "inactive";
                    details = $"Changed URL status from 'active' to 'inactive' for: {urlNames[urlIndex]}";
                }
                else if (actions[actionIndex] == "Test Ping")
                {
                    oldValue = "unknown";
                    newValue = isSuccess ? "success" : "failure";
                    details = $"Ping test for {urlNames[urlIndex]}: {(isSuccess ? "Success (245ms)" : "Failed - Timeout")}";
                }
                
                logs.Add(new AuditLog
                {
                    Id = 300 + i,
                    LogType = "URL Management",
                    Timestamp = date,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = users[userIndex],
                    UserRole = "System Admin",
                    Action = actions[actionIndex],
                    Status = isSuccess ? "success" : "failure",
                    IpAddress = $"192.168.1.{new Random().Next(100, 200)}",
                    InitiatingUser = users[userIndex],
                    TargetUser = urlNames[urlIndex],
                    ChangeType = actions[actionIndex],
                    FieldChanged = "URL Configuration",
                    OldValue = oldValue,
                    NewValue = newValue,
                    Details = details
                });
            }
            
            return logs.OrderByDescending(l => l.Timestamp).ToList();
        }

        private List<AuditLog> GetMockActivityLogs()
        {
            var logs = new List<AuditLog>();
            var users = new[] { "admin", "jsmith", "mjohnson", "awilson", "pbrown" };
            var roles = new[] { "System Admin", "User Admin", "LGU", "User", "User" };
            var actions = new[] { "login", "logout", "failed_login", "lockout", "password_change" };
            var ips = new[] { "192.168.1.100", "192.168.1.105", "192.168.2.50", "192.168.1.120", "192.168.1.130" };
            
            for (int i = 1; i <= 50; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 7)).AddHours(-new Random().Next(0, 24));
                var userIndex = new Random().Next(0, users.Length);
                var actionIndex = new Random().Next(0, actions.Length);
                var isSuccess = actions[actionIndex] == "failed_login" ? false : (new Random().Next(0, 10) > 2);
                
                logs.Add(new AuditLog
                {
                    Id = i,
                    LogType = "Activity",
                    Timestamp = date,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = users[userIndex],
                    UserRole = roles[userIndex],
                    Action = actions[actionIndex],
                    Status = isSuccess ? "success" : "failure",
                    IpAddress = ips[new Random().Next(0, ips.Length)],
                    ActivityType = actions[actionIndex],
                    Details = isSuccess 
                        ? $"{actions[actionIndex]} from {ips[new Random().Next(0, ips.Length)]}"
                        : $"Failed {actions[actionIndex]} attempt - invalid credentials"
                });
            }
            
            return logs.OrderByDescending(l => l.Timestamp).ToList();
        }

        private List<AuditLog> GetMockMaintenanceLogs()
        {
            var logs = new List<AuditLog>();
            var initiators = new[] { "admin", "jsmith" };
            var targets = new[] { "mjohnson", "awilson", "pbrown", "slee", "rjohnson" };
            var changeTypes = new[] { "create", "update", "delete", "suspend", "activate", "role_change", "password_reset" };
            var fields = new[] { "Role", "Status", "Email", "Department", "Password" };
            
            for (int i = 1; i <= 30; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 14)).AddHours(-new Random().Next(0, 24));
                var changeType = changeTypes[new Random().Next(0, changeTypes.Length)];
                var fieldChanged = fields[new Random().Next(0, fields.Length)];
                var oldValue = fieldChanged == "Role" ? "User" : 
                              fieldChanged == "Status" ? "active" : 
                              fieldChanged == "Email" ? "old@email.com" : "Old Value";
                var newValue = fieldChanged == "Role" ? "LGU" : 
                              fieldChanged == "Status" ? "inactive" : 
                              fieldChanged == "Email" ? "new@email.com" : "New Value";
                
                logs.Add(new AuditLog
                {
                    Id = 100 + i,
                    LogType = "Maintenance",
                    Timestamp = date,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = initiators[new Random().Next(0, initiators.Length)],
                    UserRole = "System Admin",
                    Action = changeType,
                    Status = "success",
                    IpAddress = "192.168.1.100",
                    InitiatingUser = initiators[new Random().Next(0, initiators.Length)],
                    TargetUser = targets[new Random().Next(0, targets.Length)],
                    TargetUserId = new Random().Next(1, 10),
                    ChangeType = changeType,
                    FieldChanged = fieldChanged,
                    OldValue = oldValue,
                    NewValue = newValue,
                    Details = $"{changeType} on {fieldChanged}: '{oldValue}' → '{newValue}'"
                });
            }
            
            return logs.OrderByDescending(l => l.Timestamp).ToList();
        }

        private List<AuditLog> GetMockParameterLogs()
        {
            var logs = new List<AuditLog>();
            var modules = new[] { "System Settings", "URL Management", "User Management", "Security", "Notifications" };
            var parameters = new[] 
            { 
                "Ping Interval", "Session Timeout", "Password Expiry", 
                "Max Login Attempts", "Retention Days", "Support Email",
                "System Name", "Dormant Days", "Password History" 
            };
            
            for (int i = 1; i <= 20; i++)
            {
                var date = DateTime.Now.AddDays(-new Random().Next(0, 30)).AddHours(-new Random().Next(0, 24));
                var paramIndex = new Random().Next(0, parameters.Length);
                var moduleIndex = new Random().Next(0, modules.Length);
                var oldValue = paramIndex == 0 ? "5" :
                              paramIndex == 1 ? "10" :
                              paramIndex == 2 ? "90" :
                              paramIndex == 3 ? "3" :
                              paramIndex == 4 ? "30" :
                              paramIndex == 5 ? "support@old.com" :
                              paramIndex == 6 ? "Old System" :
                              paramIndex == 7 ? "30" : "12";
                var newValue = paramIndex == 0 ? "3" :
                              paramIndex == 1 ? "15" :
                              paramIndex == 2 ? "60" :
                              paramIndex == 3 ? "5" :
                              paramIndex == 4 ? "45" :
                              paramIndex == 5 ? "support@new.com" :
                              paramIndex == 6 ? "New System" :
                              paramIndex == 7 ? "45" : "15";
                
                logs.Add(new AuditLog
                {
                    Id = 200 + i,
                    LogType = "Parameter",
                    Timestamp = date,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss"),
                    User = "admin",
                    UserRole = "System Admin",
                    Action = "update",
                    Status = "success",
                    IpAddress = "192.168.1.100",
                    Module = modules[moduleIndex],
                    Parameter = parameters[paramIndex],
                    OldValue = oldValue,
                    NewValue = newValue,
                    Details = $"Changed {parameters[paramIndex]} from '{oldValue}' to '{newValue}'"
                });
            }
            
            return logs.OrderByDescending(l => l.Timestamp).ToList();
        }

        public void OnGet()
        {
            ActivityLogs = GetMockActivityLogs();
            MaintenanceLogs = GetMockMaintenanceLogs();
            ParameterLogs = GetMockParameterLogs();
            UrlLogs = GetMockUrlLogs();  // ADD THIS LINE
            
            // Get unique users and actions for filters
            var allUsers = new List<string>();
            allUsers.AddRange(ActivityLogs.Select(l => l.User));
            allUsers.AddRange(MaintenanceLogs.Select(l => l.InitiatingUser));
            allUsers.AddRange(ParameterLogs.Select(l => l.User));
            allUsers.AddRange(UrlLogs.Select(l => l.User));  // ADD THIS LINE
            UniqueUsers = allUsers.Distinct().OrderBy(u => u).ToList();
            
            var allActions = new List<string>();
            allActions.AddRange(ActivityLogs.Select(l => l.Action));
            allActions.AddRange(MaintenanceLogs.Select(l => l.ChangeType));
            allActions.AddRange(ParameterLogs.Select(l => l.Action));
            allActions.AddRange(UrlLogs.Select(l => l.Action));  // ADD THIS LINE
            UniqueActions = allActions.Distinct().OrderBy(a => a).ToList();
            
            // Calculate summary statistics
            Summary = new AuditLogSummary
            {
                TotalLogs = ActivityLogs.Count + MaintenanceLogs.Count + ParameterLogs.Count + UrlLogs.Count,
                ActivityLogs = ActivityLogs.Count,
                MaintenanceLogs = MaintenanceLogs.Count,
                ParameterLogs = ParameterLogs.Count,
                SuccessCount = ActivityLogs.Count(l => l.Status == "success") + 
                               MaintenanceLogs.Count(l => l.Status == "success") + 
                               ParameterLogs.Count(l => l.Status == "success") +
                               UrlLogs.Count(l => l.Status == "success"),
                FailureCount = ActivityLogs.Count(l => l.Status == "failure") + 
                               MaintenanceLogs.Count(l => l.Status == "failure") + 
                               ParameterLogs.Count(l => l.Status == "failure") +
                               UrlLogs.Count(l => l.Status == "failure")
            };
        }

        public IActionResult OnPostExport()
        {
            TempData["SuccessMessage"] = "Export started. Your file will be downloaded shortly.";
            return RedirectToPage();
        }

        public IActionResult OnPostClearFilters()
        {
            return RedirectToPage(new
            {
                SearchTerm = (string)null,
                ActionFilter = (string)null,
                StatusFilter = (string)null,
                StartDate = (DateTime?)null,
                EndDate = (DateTime?)null,
                UserFilter = (string)null
            });
        }
    }
}