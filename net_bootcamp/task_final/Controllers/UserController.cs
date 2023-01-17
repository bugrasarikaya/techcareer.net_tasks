using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace task_final.Controllers {
    [Authorize(Roles = "User")]
    public class UserController : Controller {
        [HttpGet]
        public IActionResult Main() {
            return View();
        }
    }
}