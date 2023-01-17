using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using task_final.Models;
namespace task_final.Controllers {
    [AllowAnonymous]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            var result = context.Users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
            context.Dispose();
            if (result != null) {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                if (result.role == "Admin") {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Main", "Admin");
                }
                else if(result.role == "User") {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Main", "User");
                }
                else return RedirectToAction();
            } else return RedirectToAction();
        }
        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            context.Users.Add(user);
            context.SaveChanges();
            context.Dispose();
            return Redirect("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}