using System;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;

namespace CTBCServiceStatusDashboard.Models
{
    public class Report
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public DateTime? LastGenerated { get; set; }
        public List<string> AvailableFormats { get; set; }
    }

    public class ReportFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Format { get; set; }
        public string UserFilter { get; set; }
        public string StatusFilter { get; set; }
    }

    public class UserActivityReportData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string User { get; set; }
        public string Role { get; set; }
        public string Activity { get; set; }
        public string Status { get; set; }
        public string IpAddress { get; set; }
        public string Details { get; set; }
    }

    public class UserMaintenanceReportData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Initiator { get; set; }
        public string TargetUser { get; set; }
        public string Action { get; set; }
        public string Fieldchanged { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IpAddress { get; set; }
    }

    public class AccessRoleReportData
    {
        public string Role { get; set; }
        public int UserCount { get; set; }
        public int PermissionCount { get; set; }
        public List<string> Permissions { get; set; }
        public DateTime LastModified { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
    public class ActiveUsersReportData
    {
        public string User { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastLoginIP { get; set; }
        public int SessionDuration { get; set; }
        public string Status { get; set; }
    }

    public class PasswordExceptionReportData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string User { get; set; }
        public string ExceptionType { get; set; }
        public int Attempts { get; set; }
        public string IpAddress { get; set; }
        public string Resolution { get; set; }
    }
}
