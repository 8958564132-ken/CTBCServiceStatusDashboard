using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CTBCServiceStatusDashboard.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CTBCServiceStatusDashboard.Pages.UserAdmin
{
    [Authorize(Roles = "User Admin, LGU")]
    public class UserListModel : PageModel
    {
        private readonly ILogger<UserListModel> _logger;

        public UserListModel(ILogger<UserListModel> logger)
        {
            _logger = logger;
        }

        public List<User> Users { get; set; }

        // Mock data - Only regular users and LGU users (no admins)
        private List<User> GetMockUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "mjohnson",
                    Email = "mjohnson@ctbc.com",
                    FirstName = "Mike",
                    LastName = "Johnson",
                    Role = "LGU",
                    Status = "locked",
                    LastLogin = DateTime.Parse("2024-03-07 09:20:00"),
                    Department = "Local Government",
                    CreatedDate = DateTime.Parse("2024-01-20"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 2,
                    Username = "awilson",
                    Email = "awilson@ctbc.com",
                    FirstName = "Alice",
                    LastName = "Wilson",
                    Role = "User",
                    Status = "dormant",
                    LastLogin = DateTime.Parse("2024-02-01 11:00:00"),
                    Department = "Sales",
                    CreatedDate = DateTime.Parse("2024-01-15"),
                    CreatedBy = "jsmith"
                },
                new User
                {
                    Id = 3,
                    Username = "pbrown",
                    Email = "pbrown@ctbc.com",
                    FirstName = "Peter",
                    LastName = "Brown",
                    Role = "User",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-09 10:45:00"),
                    Department = "Marketing",
                    CreatedDate = DateTime.Parse("2024-03-01"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 4,
                    Username = "slee",
                    Email = "slee@ctbc.com",
                    FirstName = "Sarah",
                    LastName = "Lee",
                    Role = "LGU",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-08 16:30:00"),
                    Department = "Local Government",
                    CreatedDate = DateTime.Parse("2024-02-28"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 5,
                    Username = "rjohnson",
                    Email = "rjohnson@ctbc.com",
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Role = "User",
                    Status = "inactive",
                    LastLogin = DateTime.Parse("2024-02-15 13:20:00"),
                    Department = "Finance",
                    CreatedDate = DateTime.Parse("2024-01-05"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 6,
                    Username = "jdoe",
                    Email = "jdoe@ctbc.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Role = "LGU",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-09 08:30:00"),
                    Department = "Local Government",
                    CreatedDate = DateTime.Parse("2024-02-10"),
                    CreatedBy = "jsmith"
                },
                new User
                {
                    Id = 7,
                    Username = "msmith",
                    Email = "msmith@ctbc.com",
                    FirstName = "Mary",
                    LastName = "Smith",
                    Role = "User",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-08 14:15:00"),
                    Department = "HR",
                    CreatedDate = DateTime.Parse("2024-02-05"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 8,
                    Username = "kchen",
                    Email = "kchen@ctbc.com",
                    FirstName = "Kevin",
                    LastName = "Chen",
                    Role = "LGU",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-07 11:20:00"),
                    Department = "Local Government",
                    CreatedDate = DateTime.Parse("2024-02-20"),
                    CreatedBy = "admin"
                },
                new User
                {
                    Id = 9,
                    Username = "lrodriguez",
                    Email = "lrodriguez@ctbc.com",
                    FirstName = "Laura",
                    LastName = "Rodriguez",
                    Role = "User",
                    Status = "inactive",
                    LastLogin = DateTime.Parse("2024-02-28 09:45:00"),
                    Department = "Operations",
                    CreatedDate = DateTime.Parse("2024-02-01"),
                    CreatedBy = "jsmith"
                },
                new User
                {
                    Id = 10,
                    Username = "twilliams",
                    Email = "twilliams@ctbc.com",
                    FirstName = "Tom",
                    LastName = "Williams",
                    Role = "LGU",
                    Status = "active",
                    LastLogin = DateTime.Parse("2024-03-06 10:30:00"),
                    Department = "Local Government",
                    CreatedDate = DateTime.Parse("2024-02-15"),
                    CreatedBy = "admin"
                }
            };
        }

        public void OnGet()
        {
            Users = GetMockUsers().OrderBy(u => u.LastName).ToList();
        }
    }
}