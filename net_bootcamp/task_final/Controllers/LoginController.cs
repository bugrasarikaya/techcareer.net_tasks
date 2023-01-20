using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using task_final.Models;
namespace task_final.Controllers {
    [AllowAnonymous]
    public class LoginController : Controller {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger) {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Main() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(Account account) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            var result = context.Accounts.FirstOrDefault(a => a.Name == account.Name && a.Password == account.Password);
            context.Dispose();
            if (result != null) {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                if (result.Role == "Admin") {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Main", "Admin");
                }
                else if(result.Role == "User") {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Main", "User");
                }
                else return RedirectToAction("Main");
            } else return RedirectToAction("Main");
        }
        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Account account) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            account.Role = "User";
			context.Accounts.Add(account);
            context.SaveChanges();
            context.Dispose();

            return RedirectToAction("Main");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}