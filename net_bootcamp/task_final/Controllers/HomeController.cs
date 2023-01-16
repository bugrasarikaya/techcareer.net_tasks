using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task_final.Models;
namespace task_final.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            var result = context.Users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
            context.Dispose();
            if(result != null) {
                return View("Main");
            }
            else return RedirectToAction();
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
            return Redirect("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}