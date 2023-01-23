using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using task_final.Models;
using task_final.ViewModels;

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
			var result = context.Accounts.FirstOrDefault(a => a.Email == account.Email && a.Password == account.Password);
			context.Dispose();
			if (result != null) {
				ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				if (result.Role == "Admin") {
					HttpContext.Session.SetInt32("AccountID", result.ID);
					identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
					return RedirectToAction("Main", "Admin");
				} else if (result.Role == "User") {
					HttpContext.Session.SetInt32("AccountID", result.ID);
					identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
					return RedirectToAction("Main", "User");
				} else return RedirectToAction("Main");
			} else return RedirectToAction("Main");
		}
		[HttpGet]
		public IActionResult Register() {
			return View();
		}
		[HttpPost]
		public IActionResult Register(AccountRegisterViewModel account) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Accounts.Where(a => a.Email == account.Email).Count() > 0) {
				ModelState.AddModelError("Email", "Email is already exists.");
				context.Dispose();
				return View("Register");
			} else {
				context.Accounts.Add(new Account() { Email = account.Email, Name = account.Name, Surname = account.Surname, Password = account.Password, Role = "User" });
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("Main");
			}
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}