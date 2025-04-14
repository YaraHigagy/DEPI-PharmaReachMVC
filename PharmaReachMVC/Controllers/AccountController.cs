using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;
using PharmaReachMVC.ViewModels;
using System.Threading.Tasks;

namespace PharmaReachMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PharmaReachDbContext _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  PharmaReachDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        // GET: Account/Register
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserType = model.UserType
                };

                var result = await _userManager.CreateAsync(user); // no password
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.UserType);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (user.UserType == "Pharmacy")
                    {
                        // 👇 Create the Customer entity
                        var pharmacy = new Pharmacy
                        {
                            Name = model.Name,
                            Email = model.Email,
                            //Phone = model.Phone,
                            //AddressId = model.AddressId, // assuming you collect AddressId too
                            Phone = "012345678",
                            AddressId = 1,
                            ApplicationUserId = user.Id
                        };

                        _dbContext.Pharmacies.Add(pharmacy);
                        await _dbContext.SaveChangesAsync();

                        return RedirectToAction("Profile", "Pharmacy", new { id = pharmacy.Id });
                    }
                    else if (user.UserType == "Customer")
                    {
                        // 👇 Create the Customer entity
                        var customer = new Customer
                        {
                            Name = model.Name,
                            Email = model.Email,
                            //Phone = model.Phone,
                            //AddressId = model.AddressId, // assuming you collect AddressId too
                            Phone = "012345678",
                            AddressId = 1,
                            ApplicationUserId = user.Id
                        };

                        _dbContext.Customers.Add(customer);
                        await _dbContext.SaveChangesAsync();

                        return RedirectToAction("Profile", "Customer", new { id = customer.Id });
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: Account/Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                // Check if the user exists
                if (user != null)
                {
                    // Sign the user in directly (no password required)
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect based on UserType
                    if (user.UserType == "Pharmacy")
                    {
                        // Get the Pharmacy associated with the user
                        var pharmacy = await _dbContext.Pharmacies.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
                        if (pharmacy != null)
                        {
                            return RedirectToAction("Profile", "Pharmacy", new { id = pharmacy.Id });
                        }
                    }
                    else if (user.UserType == "Customer")
                    {
                        // Get the Customer associated with the user
                        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);
                        if (customer != null)
                        {
                            return RedirectToAction("Profile", "Customer", new { id = customer.Id });
                        }
                    }
                }
                else
                {
                    // If user is not found, show an error message
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            // If we get here, something went wrong. Redisplay the login form.
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign the user out
            await _signInManager.SignOutAsync();

            // Explicitly clear the authentication cookie
            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            // Optionally clear other cookies (e.g., session cookies)
            Response.Cookies.Delete(".AspNetCore.Session");

            // Redirect the user to the Medicines page after logging out
            return RedirectToAction("Index", "Medicines");
        }

    }
}
