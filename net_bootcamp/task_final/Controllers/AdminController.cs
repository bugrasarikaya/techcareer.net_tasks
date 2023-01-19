using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_final.Models;
using task_final.ViewModels;

namespace task_final.Controllers {
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller {
		[HttpGet]
		public IActionResult Main() {
			return View();
		}
		[HttpGet]
		public IActionResult Users() {
			return View();
		}
		[HttpGet]
		public IActionResult Products() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ProductImageGetViewModel product_image_get_view_model = new ProductImageGetViewModel();
			var data_1 = context.Products.Select(c => new Product() { ID = c.ID, Name = c.Name, Category = c.Category, Description = c.Description, Price = c.Price, ImageID = c.ImageID }).ToList();
			List<ProductImageGetViewModel> list = new List<ProductImageGetViewModel>();
			foreach (Product product in data_1) {
				var data_2 = context.Images.Single(c => c.ID == product.ImageID);
				list.Add(new ProductImageGetViewModel() { ProductID = product.ID, ProductName = product.Name, ProductCategory = product.Category, ProductDescription = product.Description, ProductPrice = product.Price, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(data_2.Binary)) });
			}
			context.Dispose();
			return View(list);
		}
		[HttpGet]
		public IActionResult AddProduct() {
			return View();
		}
		[HttpPost]
		public IActionResult AddProduct(ProductImageSetViewModel product_image) {
			BinaryReader binary_reader = new BinaryReader(product_image.ImageBinary.OpenReadStream());
			Image image = new Image() { Name = product_image.ProductName, Binary = binary_reader.ReadBytes((int)product_image.ImageBinary.Length) };
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Images.Add(image);
			context.SaveChanges();
			Product product = new Product() { Name = product_image.ProductName, Category = product_image.ProductCategory, ImageID = image.ID, Description = product_image.ProductDescription, Price = product_image.ProductPrice };
			context.Products.Add(product);
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("Products");
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}