using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace task_final.Controllers {
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller {
		[HttpGet]
		public IActionResult Main() {
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}