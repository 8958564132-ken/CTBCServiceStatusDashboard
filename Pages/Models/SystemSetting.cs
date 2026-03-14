using System;
using System.Collections.Generic;

namespace CTBCServiceStatusDashboard.Models
{
    public class SystemSetting
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool Editable { get; set; }
        public string DataType { get; set; } // "string", "number", "boolean", "select"
        public List<string> Options { get; set; } // For select type
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class SourceOfFund
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // "active", "inactive"
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
    }
}