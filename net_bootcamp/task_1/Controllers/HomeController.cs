using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task_1.Models;

namespace task_1.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        public IActionResult Index() {
            NorthwindContext context = new NorthwindContext();
            var data = context.Customers.Select(c => new Customer() { CustomerId = c.CustomerId, CompanyName = c.CompanyName, ContactName = c.ContactName, City = c.City, Country = c.Country }).ToList();
            context.Dispose();
            return View(data);
        }
        public IActionResult Create() {
            return View();
        }
        public IActionResult Add(Customer customer) {
            NorthwindContext context = new NorthwindContext();
            context.Customers.Add(customer);
            context.SaveChanges();
            context.Dispose();
            return Redirect("Index");
        }
        public IActionResult Details(string CustomerId) {
            NorthwindContext context = new NorthwindContext();
            var data = context.Customers.Single(c => c.CustomerId == CustomerId);
            context.Dispose();
            return View(data);
        }
        public IActionResult Privacy() {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}