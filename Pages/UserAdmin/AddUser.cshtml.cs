using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CTBCServiceStatusDashboard.Models;
using System;
using System.Text.RegularExpressions;

namespace CTBCServiceStatusDashboard.Pages.UserAdmin
{
    [Authorize(Roles = "User Admin, LGU")]
    public class AddUserModel : PageModel
    {
        private readonly ILogger<AddUserModel> _logger;

        public AddUserModel(ILogger<AddUserModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public bool IsEdit => User?.Id > 0;
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                // Load user for editing
                LoadMockUser(id.Value);
            }
            else
            {
                User = new User
                {
                    Status = "active",
                    Role = "User"
                };
            }
        }

        private void LoadMockUser(int id)
        {
            // Mock data - In production, this would come from database
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Username = "mjohnson",
                    Email = "mjohnson@ctbc.com",
                    FirstName = "Mike",
                    LastName = "Johnson",
                    Role = "LGU",
                    Status = "active",
                    Department = "Local Government",
                    ContactNumber = "123-456-7890",
                    Remarks = "LGU representative"
                },
                new User
                {
                    Id = 2,
                    Username = "awilson",
                    Email = "awilson@ctbc.com",
                    FirstName = "Alice",
                    LastName = "Wilson",
                    Role = "User",
                    Status = "active",
                    Department = "Sales",
                    ContactNumber = "123-456-7891",
                    Remarks = "Sales department"
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
                    Department = "Marketing",
                    ContactNumber = "123-456-7892",
                    Remarks = "Marketing manager"
                }
            };

            var foundUser = Array.Find(users, u => u.Id == id);
            if (foundUser != null)
            {
                User = foundUser;
            }
            else
            {
                User = new User
                {
                    Status = "active",
                    Role = "User"
                };
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
                
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            // Check for at least one number
            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            // Check for at least one special character
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?"":{}|<>]"))
                return false;

            return true;
        }

        public IActionResult OnPost()
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(User.FirstName) || 
                    string.IsNullOrEmpty(User.LastName) ||
                    string.IsNullOrEmpty(User.Username) || 
                    string.IsNullOrEmpty(User.Email) ||
                    string.IsNullOrEmpty(User.Role))
                {
                    ErrorMessage = "Please fill in all required fields";
                    return Page();
                }

                // Validate email format
                if (!IsValidEmail(User.Email))
                {
                    ErrorMessage = "Invalid email format";
                    return Page();
                }

                // Validate role (prevent creating admin roles)
                if (User.Role != "User" && User.Role != "LGU")
                {
                    ErrorMessage = "User Admins can only create Regular Users and LGU Users";
                    return Page();
                }

                if (!IsEdit)
                {
                    // Validate password for new users
                    if (string.IsNullOrEmpty(Password))
                    {
                        ErrorMessage = "Password is required";
                        return Page();
                    }

                    if (Password.Length < 8)
                    {
                        ErrorMessage = "Password must be at least 8 characters long";
                        return Page();
                    }

                    if (!IsStrongPassword(Password))
                    {
                        ErrorMessage = "Password must contain at least one number and one special character";
                        return Page();
                    }

                    if (Password != ConfirmPassword)
                    {
                        ErrorMessage = "Passwords do not match";
                        return Page();
                    }

                    // In production, hash password and save to database
                    SuccessMessage = $"User {User.FirstName} {User.LastName} created successfully";
                    _logger.LogInformation("New user created: {Username}", User.Username);
                }
                else
                {
                    // In production, update user in database
                    SuccessMessage = $"User {User.FirstName} {User.LastName} updated successfully";
                    _logger.LogInformation("User updated: {Username}", User.Username);
                }

                // Redirect to user list after successful operation
                return RedirectToPage("UserList", new { successMessage = SuccessMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving user");
                ErrorMessage = "An error occurred while saving the user";
                return Page();
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("UserList");
        }
    }
}