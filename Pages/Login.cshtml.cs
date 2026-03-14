using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTBCServiceStatusDashboard.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ErrorMessage = null;
            
            // If user is already logged in, redirect to appropriate dashboard
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("System Admin"))
                {
                    return RedirectToPage("/SystemAdmin/Dashboard");
                }
                else if (User.IsInRole("User Admin"))
                {
                    return RedirectToPage("/UserAdmin/Dashboard");
                }
                else if (User.IsInRole("LGU"))
                {
                    return RedirectToPage("/LGU/Dashboard");
                }
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            // System Admin login
            if (Username == "admin" && Password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Email, "admin@ctbc.com"),
                    new Claim(ClaimTypes.Role, "System Admin"),
                    new Claim("UserId", "1"),
                    new Claim("FullName", "System Administrator")
                };

                await SignInUser(claims);
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                
                return RedirectToPage("/SystemAdmin/Dashboard");
            }
            
            // User Admin login
            else if (Username == "useradmin" && Password == "user123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Email, "useradmin@ctbc.com"),
                    new Claim(ClaimTypes.Role, "User Admin"),
                    new Claim("UserId", "2"),
                    new Claim("FullName", "User Administrator")
                };

                await SignInUser(claims);
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                
                return RedirectToPage("/UserAdmin/Dashboard");
            }
            
            // LGU User login
            else if (Username == "lgu" && Password == "lgu123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Email, "lgu@ctbc.com"),
                    new Claim(ClaimTypes.Role, "LGU"),
                    new Claim("UserId", "3"),
                    new Claim("FullName", "LGU User")
                };

                await SignInUser(claims);
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                
                return RedirectToPage("/LGU/Dashboard");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        private async Task SignInUser(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}