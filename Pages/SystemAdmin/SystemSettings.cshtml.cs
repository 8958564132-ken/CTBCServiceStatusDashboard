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
    public class SystemSettingsModel : PageModel
    {
        private readonly ILogger<SystemSettingsModel> _logger;

        public SystemSettingsModel(ILogger<SystemSettingsModel> logger)
        {
            _logger = logger;
        }

        public List<SourceOfFund> SourceOfFunds { get; set; }
        public Dictionary<string, List<SystemSetting>> SystemParameters { get; set; }
        
        [BindProperty]
        public SourceOfFund NewFund { get; set; }
        
        [BindProperty]
        public SourceOfFund EditFund { get; set; }
        
        [BindProperty]
        public SystemSetting EditParameter { get; set; }
        
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        private List<SourceOfFund> GetMockSourceOfFunds()
        {
            return new List<SourceOfFund>
            {
                new SourceOfFund
                {
                    Id = 1,
                    Name = "Department of Energy",
                    Code = "DOE",
                    Description = "Energy sector projects and initiatives",
                    Status = "active",
                    CreatedDate = DateTime.Parse("2024-01-01"),
                    UpdatedDate = DateTime.Parse("2024-01-15"),
                    CreatedBy = "admin",
                    Remarks = "Primary funding source for energy projects"
                },
                new SourceOfFund
                {
                    Id = 2,
                    Name = "Agricultural Programs and Initiatives",
                    Code = "APRI",
                    Description = "Agricultural development programs",
                    Status = "active",
                    CreatedDate = DateTime.Parse("2024-01-01"),
                    UpdatedDate = DateTime.Parse("2024-01-14"),
                    CreatedBy = "admin",
                    Remarks = "Rural development funding"
                },
                new SourceOfFund
                {
                    Id = 3,
                    Name = "Department of Agriculture",
                    Code = "DA",
                    Description = "Agriculture and fisheries",
                    Status = "active",
                    CreatedDate = DateTime.Parse("2024-01-05"),
                    UpdatedDate = DateTime.Parse("2024-01-10"),
                    CreatedBy = "admin",
                    Remarks = "Agricultural sector funding"
                },
                new SourceOfFund
                {
                    Id = 4,
                    Name = "Department of Public Works",
                    Code = "DPWH",
                    Description = "Infrastructure projects",
                    Status = "inactive",
                    CreatedDate = DateTime.Parse("2024-01-08"),
                    UpdatedDate = DateTime.Parse("2024-01-12"),
                    CreatedBy = "jsmith",
                    Remarks = "Infrastructure development"
                },
                new SourceOfFund
                {
                    Id = 5,
                    Name = "Department of Education",
                    Code = "DepEd",
                    Description = "Educational programs",
                    Status = "active",
                    CreatedDate = DateTime.Parse("2024-01-10"),
                    UpdatedDate = DateTime.Parse("2024-01-10"),
                    CreatedBy = "admin",
                    Remarks = "Education sector funding"
                }
            };
        }

        private Dictionary<string, List<SystemSetting>> GetMockSystemParameters()
        {
            var parameters = new Dictionary<string, List<SystemSetting>>();

            // Monitoring Settings
            parameters["Monitoring"] = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Id = "ping_interval",
                    Category = "Monitoring",
                    Name = "Ping Interval",
                    Value = "5",
                    Description = "Frequency of automatic ping checks (minutes)",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-03-01"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "ping_timeout",
                    Category = "Monitoring",
                    Name = "Ping Timeout",
                    Value = "15",
                    Description = "Maximum time to wait for ping response (seconds)",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-03-01"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "retention_days",
                    Category = "Monitoring",
                    Name = "Data Retention",
                    Value = "30",
                    Description = "Number of days to keep ping history",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-15"),
                    ModifiedBy = "admin"
                }
            };

            // Security Settings
            parameters["Security"] = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Id = "session_timeout",
                    Category = "Security",
                    Name = "Session Timeout",
                    Value = "10",
                    Description = "Idle session timeout (minutes)",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-03-05"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "max_login_attempts",
                    Category = "Security",
                    Name = "Max Login Attempts",
                    Value = "3",
                    Description = "Number of failed attempts before lockout",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-03-05"),
                    ModifiedBy = "admin"
                }
            };

            // Password Policy
            parameters["Password Policy"] = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Id = "min_password_length",
                    Category = "Password Policy",
                    Name = "Minimum Password Length",
                    Value = "8",
                    Description = "Minimum characters required for password",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-20"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "require_special_chars",
                    Category = "Password Policy",
                    Name = "Require Special Characters",
                    Value = "true",
                    Description = "Password must contain special characters",
                    Editable = true,
                    DataType = "boolean",
                    LastModified = DateTime.Parse("2024-02-20"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "admin_password_expiry",
                    Category = "Password Policy",
                    Name = "Admin Password Expiry",
                    Value = "30",
                    Description = "Days until admin password expires",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-20"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "user_password_expiry",
                    Category = "Password Policy",
                    Name = "User Password Expiry",
                    Value = "90",
                    Description = "Days until user password expires",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-20"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "password_history",
                    Category = "Password Policy",
                    Name = "Password History",
                    Value = "12",
                    Description = "Number of previous passwords to remember",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-20"),
                    ModifiedBy = "admin"
                }
            };

            // User Management
            parameters["User Management"] = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Id = "dormant_days",
                    Category = "User Management",
                    Name = "Dormant Account Days",
                    Value = "30",
                    Description = "Days of inactivity before account is dormant",
                    Editable = true,
                    DataType = "number",
                    LastModified = DateTime.Parse("2024-02-25"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "default_role",
                    Category = "User Management",
                    Name = "Default Role",
                    Value = "User",
                    Description = "Default role for new users",
                    Editable = true,
                    DataType = "select",
                    Options = new List<string> { "User", "LGU", "User Admin" },
                    LastModified = DateTime.Parse("2024-02-25"),
                    ModifiedBy = "admin"
                }
            };

            // System
            parameters["System"] = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Id = "system_name",
                    Category = "System",
                    Name = "System Name",
                    Value = "CTBC Service Status Dashboard",
                    Description = "Application display name",
                    Editable = true,
                    DataType = "string",
                    LastModified = DateTime.Parse("2024-01-01"),
                    ModifiedBy = "system"
                },
                new SystemSetting
                {
                    Id = "support_email",
                    Category = "System",
                    Name = "Support Email",
                    Value = "support@ctbc.com",
                    Description = "Email for user support",
                    Editable = true,
                    DataType = "string",
                    LastModified = DateTime.Parse("2024-01-01"),
                    ModifiedBy = "admin"
                },
                new SystemSetting
                {
                    Id = "company_name",
                    Category = "System",
                    Name = "Company Name",
                    Value = "CTBC Bank",
                    Description = "Company/Organization name",
                    Editable = true,
                    DataType = "string",
                    LastModified = DateTime.Parse("2024-01-01"),
                    ModifiedBy = "admin"
                }
            };

            return parameters;
        }

        public void OnGet()
        {
            SourceOfFunds = GetMockSourceOfFunds();
            SystemParameters = GetMockSystemParameters();
        }

        public IActionResult OnPostAddFund()
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(NewFund.Name) || string.IsNullOrEmpty(NewFund.Code))
                {
                    ErrorMessage = "Fund Name and Code are required";
                    OnGet();
                    return Page();
                }

                // In production, save to database
                SuccessMessage = $"Source of Fund '{NewFund.Name}' added successfully";
                
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding source of fund");
                ErrorMessage = "An error occurred while adding the source of fund";
                OnGet();
                return Page();
            }
        }

        public IActionResult OnPostEditFund(int id)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(EditFund.Name) || string.IsNullOrEmpty(EditFund.Code))
                {
                    ErrorMessage = "Fund Name and Code are required";
                    OnGet();
                    return Page();
                }

                // In production, update in database
                SuccessMessage = $"Source of Fund '{EditFund.Name}' updated successfully";
                
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating source of fund");
                ErrorMessage = "An error occurred while updating the source of fund";
                OnGet();
                return Page();
            }
        }

        public IActionResult OnPostDeleteFund(int id)
        {
            try
            {
                SuccessMessage = "Source of Fund deleted successfully";
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting source of fund");
                ErrorMessage = "An error occurred while deleting the source of fund";
                OnGet();
                return Page();
            }
        }

        public IActionResult OnPostToggleFundStatus(int id)
        {
            try
            {
                SuccessMessage = "Source of Fund status updated successfully";
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling fund status");
                ErrorMessage = "An error occurred while updating fund status";
                OnGet();
                return Page();
            }
        }

        public IActionResult OnPostSaveParameter()
        {
            try
            {
                SuccessMessage = $"Parameter '{EditParameter.Name}' updated successfully";
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving parameter");
                ErrorMessage = "An error occurred while saving the parameter";
                OnGet();
                return Page();
            }
        }
    }
}