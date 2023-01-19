using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
		public IActionResult ProductList() {
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
		public IActionResult ProductAdd() {
			return View();
		}
		[HttpPost]
		public IActionResult ProductAdd(ProductImageSetViewModel product_image) {
			BinaryReader binary_reader = new BinaryReader(product_image.ImageBinary.OpenReadStream());
			Image image = new Image() { Name = product_image.ProductName, Binary = binary_reader.ReadBytes((int)product_image.ImageBinary.Length) };
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Images.Add(image);
			context.SaveChanges();
			Product product = new Product() { Name = product_image.ProductName, Category = product_image.ProductCategory, ImageID = image.ID, Description = product_image.ProductDescription, Price = product_image.ProductPrice };
			context.Products.Add(product);
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ProductList");
		}
		[HttpGet]
		public IActionResult ProductDetails(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			var data_1 = context.Products.Single(c => c.ID == id);
			var data_2 = context.Images.Single(c => c.ID == id);
			context.Dispose();
			return View(new ProductImageViewModel() { ProductID = data_1.ID, ProductName = data_1.Name, ProductCategory = data_1.Category, ProductDescription = data_1.Description, ProductPrice = data_1.Price, ImageID = data_2.ID, ImageName = data_2.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(data_2.Binary)) });
		}
		[HttpGet]
		public IActionResult ProductEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			var data_1 = context.Products.Single(c => c.ID == id);
			var data_2 = context.Images.Single(c => c.ID == id);
			context.Dispose();
			return View(new ProductImageViewModel() { ProductID = data_1.ID, ProductName = data_1.Name, ProductCategory = data_1.Category, ProductDescription = data_1.Description, ProductPrice = data_1.Price, ImageID = data_2.ID, ImageName = data_2.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(data_2.Binary)) });
		}
		[HttpPost]
		public IActionResult ProductEdit(ProductImageViewModel product_image_view_model) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Product product = context.Products.Single(c => c.ID == product_image_view_model.ProductID);
			product.Name = product_image_view_model.ProductName;
			product.Category = product_image_view_model.ProductCategory;
			product.Description = product_image_view_model.ProductDescription;
			product.Price = product_image_view_model.ProductPrice;
			Image image = context.Images.Single(c => c.ID == product_image_view_model.ImageID);
			image.Name = product_image_view_model.ProductName;
			if (product_image_view_model.ImageBinary != null) {
				BinaryReader binary_reader = new BinaryReader(product_image_view_model.ImageBinary.OpenReadStream());
				image.Binary = binary_reader.ReadBytes((int)product_image_view_model.ImageBinary.Length);
			}
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ProductList");
		}
		[HttpGet]
		public IActionResult ProductDelete(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Products.Remove(context.Products.Single(c => c.ID == id));
			context.Images.Remove(context.Images.Single(c => c.ID == id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ProductList");
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}