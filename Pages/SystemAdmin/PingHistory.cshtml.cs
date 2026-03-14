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
    public class PingHistoryModel : PageModel
    {
        private readonly ILogger<PingHistoryModel> _logger;

        public PingHistoryModel(ILogger<PingHistoryModel> logger)
        {
            _logger = logger;
        }

        public List<PingResult> PingResults { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string UrlFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }
        
        public List<string> UniqueUrls { get; set; }
        
        // Summary statistics
        public int TotalPings { get; set; }
        public int SuccessPings { get; set; }
        public int ErrorPings { get; set; }
        public int TimeoutPings { get; set; }
        public double AvgResponseTime { get; set; }

        // Mock data - In production, this would come from a database
        private List<PingResult> GetMockPingHistory()
        {
            var results = new List<PingResult>();
            var random = new Random();
            var urls = new[] { "Main API", "Database", "Payment Gateway", "Auth Service", "Storage API", "CDN" };
            var servers = new[] { "api-server-01", "db-server-01", "pay-server-01", "auth-server-01", "store-server-01", "cdn-server-01" };
            var ipAddresses = new[] { "192.168.1.100", "192.168.1.200", "192.168.1.30", "192.168.1.40", "192.168.1.50", "192.168.1.60" };
            var ports = new[] { 443, 3306, 8443, 8080, 9000, 80 };
            
            // Generate data for the last 7 days
            for (int i = 0; i < 100; i++)
            {
                var date = DateTime.Now.AddDays(-random.Next(0, 7)).AddHours(-random.Next(0, 24)).AddMinutes(-random.Next(0, 60));
                var urlIndex = random.Next(0, urls.Length);
                var status = random.Next(0, 10) > 2 ? "success" : (random.Next(0, 2) == 0 ? "error" : "timeout");
                var responseTime = status == "success" ? random.Next(50, 2000) : (int?)null;
                var statusCode = status == "success" ? 200 : (status == "error" ? 500 : 408);
                
                results.Add(new PingResult
                {
                    Id = i + 1,
                    UrlId = urlIndex + 1,
                    UrlName = urls[urlIndex],
                    Status = status,
                    ResponseTime = responseTime,
                    StatusCode = statusCode,
                    ErrorMessage = status != "success" ? (status == "error" ? "Connection refused" : "Request timeout") : null,
                    Timestamp = date,
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = date.ToString("HH:mm:ss")
                });
            }
            
            return results.OrderByDescending(p => p.Timestamp).ToList();
        }

        public void OnGet()
        {
            var allResults = GetMockPingHistory();
            UniqueUrls = allResults.Select(p => p.UrlName).Distinct().ToList();
            
            // Apply filters
            var query = allResults.AsQueryable();
            
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(p => 
                    p.UrlName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.ErrorMessage != null && p.ErrorMessage.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }
            
            if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "all")
            {
                query = query.Where(p => p.Status == StatusFilter);
            }
            
            if (!string.IsNullOrEmpty(UrlFilter) && UrlFilter != "all")
            {
                query = query.Where(p => p.UrlName == UrlFilter);
            }
            
            if (StartDate.HasValue)
            {
                var start = StartDate.Value.Date;
                query = query.Where(p => p.Timestamp.Date >= start);
            }
            
            if (EndDate.HasValue)
            {
                var end = EndDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(p => p.Timestamp <= end);
            }
            
            PingResults = query.ToList();
            
            // Calculate summary statistics
            TotalPings = PingResults.Count;
            SuccessPings = PingResults.Count(p => p.Status == "success");
            ErrorPings = PingResults.Count(p => p.Status == "error");
            TimeoutPings = PingResults.Count(p => p.Status == "timeout");
            
            if (SuccessPings > 0)
            {
                AvgResponseTime = PingResults
                    .Where(p => p.ResponseTime.HasValue)
                    .Average(p => p.ResponseTime.Value);
            }
        }

        public IActionResult OnPostExport()
        {
            // In production, this would generate and return a CSV/Excel file
            TempData["SuccessMessage"] = "Export started. Your file will be downloaded shortly.";
            return RedirectToPage();
        }

        public IActionResult OnPostClearFilters()
        {
            return RedirectToPage(new
            {
                SearchTerm = (string)null,
                StatusFilter = (string)null,
                UrlFilter = (string)null,
                StartDate = (DateTime?)null,
                EndDate = (DateTime?)null
            });
        }
    }
}