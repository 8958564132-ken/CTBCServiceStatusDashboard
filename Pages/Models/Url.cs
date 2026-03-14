using System;
using System.ComponentModel.DataAnnotations;

namespace CTBCServiceStatusDashboard.Models
{
    public class Url
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Server")]
        public string Server { get; set; }

        [Required]
        [Display(Name = "IP Address")]
        [RegularExpression(@"^(\d{1,3}\.){3}\d{1,3}$", ErrorMessage = "Invalid IP address format")]
        public string IpAddress { get; set; }

        [Required]
        [Range(1, 65535)]
        [Display(Name = "Port")]
        public int Port { get; set; }

        [Required]
        [Display(Name = "URL/Path")]
        public string UrlPath { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "active"; // "active", "inactive"

        [Display(Name = "Last Ping")]
        public string LastPing { get; set; }

        [Display(Name = "Last Ping Status")]
        public string LastPingStatus { get; set; } // "success", "error", "pending"

        [Display(Name = "Last Checked")]
        public string LastChecked { get; set; }

        [Display(Name = "Response Time")]
        public int? ResponseTime { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    public class PingResult
    {
        public int Id { get; set; }
        public int UrlId { get; set; }
        public string UrlName { get; set; }
        public string Status { get; set; }
        public int? ResponseTime { get; set; }
        public int? StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}