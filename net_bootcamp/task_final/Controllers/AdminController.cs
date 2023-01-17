using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace task_final.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        [HttpGet]
        public IActionResult Main() {
            return View();
        }
    }
}