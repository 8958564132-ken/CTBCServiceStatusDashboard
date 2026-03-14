using System;

namespace CTBCServiceStatusDashboard.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string LogType { get; set; } // "Activity", "Maintenance", "Parameter"
        public DateTime Timestamp { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        
        // Common fields
        public string User { get; set; }
        public string UserRole { get; set; }
        public string Action { get; set; }
        public string Status { get; set; } // "success", "failure"
        public string IpAddress { get; set; }
        public string Details { get; set; }
        
        // For Activity Logs
        public string ActivityType { get; set; } // "login", "logout", "failed_login", "lockout", "password_change"
        
        // For Maintenance Logs
        public string InitiatingUser { get; set; }
        public string TargetUser { get; set; }
        public int? TargetUserId { get; set; }  // ← ADD THIS LINE (nullable int)
        public string ChangeType { get; set; } // "create", "update", "delete", "suspend", "activate", "role_change", "password_reset"
        public string FieldChanged { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        
        // For Parameter Logs
        public string Module { get; set; }
        public string Parameter { get; set; }
    }

    public class AuditLogSummary
    {
        public int TotalLogs { get; set; }
        public int ActivityLogs { get; set; }
        public int MaintenanceLogs { get; set; }
        public int ParameterLogs { get; set; }
        public int UrlLogs { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}