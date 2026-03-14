using System;
using System.ComponentModel.DataAnnotations;

namespace CTBCServiceStatusDashboard.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public string PasswordHash { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; } // "System Admin", "User Admin", "LGU", "User"

        [Display(Name = "Status")]
        public string Status { get; set; } // "active", "inactive", "dormant", "locked"

        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }

        [Display(Name = "Last Login IP")]
        public string LastLoginIP { get; set; }

        [Display(Name = "Login Attempts")]
        public int LoginAttempts { get; set; }

        [Display(Name = "Password Last Changed")]
        public DateTime PasswordLastChanged { get; set; }

        [Display(Name = "Password Expiry Date")]
        public DateTime PasswordExpiryDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}