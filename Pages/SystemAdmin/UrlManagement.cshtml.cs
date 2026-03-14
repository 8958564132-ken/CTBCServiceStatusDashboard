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
    public class UrlManagementModel : PageModel
    {
        private readonly ILogger<UrlManagementModel> _logger;

        public UrlManagementModel(ILogger<UrlManagementModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Url NewUrl { get; set; }

        [BindProperty]
        public Url EditUrl { get; set; }

        public List<Url> Urls { get; set; }
        public string SearchTerm { get; set; }
        public string StatusFilter { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        // Mock data - In production, this would come from a database
        private List<Url> GetMockUrls()
        {
            return new List<Url>
            {
                new Url
                {
                    Id = 1,
                    Name = "Main API",
                    Server = "api-server-01",
                    IpAddress = "192.168.1.100",
                    Port = 443,
                    UrlPath = "/health",
                    Status = "active",
                    LastPing = "200 OK",
                    LastPingStatus = "success",
                    LastChecked = "2 min ago",
                    ResponseTime = 120,
                    CreatedAt = DateTime.Parse("2024-01-01"),
                    UpdatedAt = DateTime.Parse("2024-01-15"),
                    CreatedBy = "admin",
                    Remarks = "Production API gateway"
                },
                new Url
                {
                    Id = 2,
                    Name = "Database",
                    Server = "db-server-01",
                    IpAddress = "192.168.1.200",
                    Port = 3306,
                    UrlPath = "/status",
                    Status = "active",
                    LastPing = "1.2s",
                    LastPingStatus = "success",
                    LastChecked = "3 min ago",
                    ResponseTime = 1200,
                    CreatedAt = DateTime.Parse("2024-01-01"),
                    UpdatedAt = DateTime.Parse("2024-01-15"),
                    CreatedBy = "admin"
                },
                new Url
                {
                    Id = 3,
                    Name = "Payment Gateway",
                    Server = "pay-server-01",
                    IpAddress = "192.168.1.30",
                    Port = 8443,
                    UrlPath = "/check",
                    Status = "inactive",
                    LastPing = "Timeout",
                    LastPingStatus = "error",
                    LastChecked = "5 min ago",
                    CreatedAt = DateTime.Parse("2024-01-02"),
                    UpdatedAt = DateTime.Parse("2024-01-14"),
                    CreatedBy = "admin",
                    Remarks = "Experiencing issues"
                },
                new Url
                {
                    Id = 4,
                    Name = "Auth Service",
                    Server = "auth-server-01",
                    IpAddress = "192.168.1.40",
                    Port = 8080,
                    UrlPath = "/auth/health",
                    Status = "active",
                    LastPing = "150ms",
                    LastPingStatus = "success",
                    LastChecked = "4 min ago",
                    ResponseTime = 150,
                    CreatedAt = DateTime.Parse("2024-01-01"),
                    UpdatedAt = DateTime.Parse("2024-01-15"),
                    CreatedBy = "admin"
                },
                new Url
                {
                    Id = 5,
                    Name = "Storage API",
                    Server = "store-server-01",
                    IpAddress = "192.168.1.50",
                    Port = 9000,
                    UrlPath = "/health",
                    Status = "active",
                    LastPing = "450ms",
                    LastPingStatus = "success",
                    LastChecked = "7 min ago",
                    ResponseTime = 450,
                    CreatedAt = DateTime.Parse("2024-01-03"),
                    UpdatedAt = DateTime.Parse("2024-01-15"),
                    CreatedBy = "jsmith"
                },
                new Url
                {
                    Id = 6,
                    Name = "CDN",
                    Server = "cdn-server-01",
                    IpAddress = "192.168.1.60",
                    Port = 80,
                    UrlPath = "/status",
                    Status = "active",
                    LastPing = "200 OK",
                    LastPingStatus = "success",
                    LastChecked = "10 min ago",
                    ResponseTime = 80,
                    CreatedAt = DateTime.Parse("2024-01-04"),
                    UpdatedAt = DateTime.Parse("2024-01-15"),
                    CreatedBy = "admin"
                }
            };
        }

        public void OnGet(string searchTerm = null, string statusFilter = null)
        {
            SearchTerm = searchTerm;
            StatusFilter = statusFilter;
            
            var allUrls = GetMockUrls();
            
            // Apply filters
            var query = allUrls.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => 
                    u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.Server.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.IpAddress.Contains(searchTerm));
            }
            
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "all")
            {
                query = query.Where(u => u.Status == statusFilter);
            }
            
            Urls = query.ToList();
        }

        public IActionResult OnPostAdd()
        {
            try
            {
                // Validate IP address format
                if (!System.Text.RegularExpressions.Regex.IsMatch(NewUrl.IpAddress, @"^(\d{1,3}\.){3}\d{1,3}$"))
                {
                    ErrorMessage = "Invalid IP address format";
                    OnGet(SearchTerm, StatusFilter);
                    return Page();
                }

                // Validate port range
                if (NewUrl.Port < 1 || NewUrl.Port > 65535)
                {
                    ErrorMessage = "Port must be between 1 and 65535";
                    OnGet(SearchTerm, StatusFilter);
                    return Page();
                }

                // In production, save to database
                SuccessMessage = "URL added successfully";
                
                // Refresh the list
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding URL");
                ErrorMessage = "An error occurred while adding the URL";
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
        }

        public IActionResult OnPostEdit(int id)
        {
            try
            {
                // Validate IP address format
                if (!System.Text.RegularExpressions.Regex.IsMatch(EditUrl.IpAddress, @"^(\d{1,3}\.){3}\d{1,3}$"))
                {
                    ErrorMessage = "Invalid IP address format";
                    OnGet(SearchTerm, StatusFilter);
                    return Page();
                }

                // Validate port range
                if (EditUrl.Port < 1 || EditUrl.Port > 65535)
                {
                    ErrorMessage = "Port must be between 1 and 65535";
                    OnGet(SearchTerm, StatusFilter);
                    return Page();
                }

                // In production, update in database
                SuccessMessage = "URL updated successfully";
                
                // Refresh the list
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating URL");
                ErrorMessage = "An error occurred while updating the URL";
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                // In production, delete from database
                SuccessMessage = "URL deleted successfully";
                
                // Refresh the list
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting URL");
                ErrorMessage = "An error occurred while deleting the URL";
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
        }

        public IActionResult OnPostToggleStatus(int id)
        {
            try
            {
                // In production, toggle status in database
                SuccessMessage = "URL status updated successfully";
                
                // Refresh the list
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling URL status");
                ErrorMessage = "An error occurred while updating URL status";
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
        }

        public IActionResult OnPostTestPing(int id)
        {
            try
            {
                // Simulate ping test
                var random = new Random();
                var success = random.Next(0, 10) > 3; // 70% success rate
                
                if (success)
                {
                    SuccessMessage = $"Ping test successful. Response time: {random.Next(50, 500)}ms";
                }
                else
                {
                    ErrorMessage = "Ping test failed: Connection timeout";
                }
                
                // Refresh the list
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing ping");
                ErrorMessage = "An error occurred while testing the URL";
                OnGet(SearchTerm, StatusFilter);
                return Page();
            }
        }
    }
}