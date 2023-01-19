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
        public IActionResult AccountList() {
            ShoppingListDbContext context = new ShoppingListDbContext();
            List<Account> list_account = context.Accounts.Select(a => new Account() { ID = a.ID, AccountName = a.AccountName, Password = a.Password, Role = a.Role }).ToList();
            context.Dispose();
            return View(list_account);
        }
        [HttpGet]
        public IActionResult AccountCreate() {
            return View();
        }
        [HttpPost]
        public IActionResult AccountCreate(Account account) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            context.Accounts.Add(account);
            context.SaveChanges();
            context.Dispose();
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public IActionResult AccountDetails(int id) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            Account account = context.Accounts.Single(a => a.ID == id);
            context.Dispose();
            return View(account);
        }
        [HttpGet]
        public IActionResult AccountEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == id);
			context.Dispose();
			return View(account);
		}
        [HttpPost]
        public IActionResult AccountEdit(Account account) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            Account account_original = context.Accounts.Single(a => a.ID == account.ID);
            account_original.AccountName = account.AccountName;
            account_original.Password = account.Password;
            account_original.Role = account.Role;
            context.SaveChanges();
            context.Dispose();
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public IActionResult AccountDelete(int id) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            context.Remove(context.Accounts.Single(a => a.ID == id));
            context.SaveChanges();
            context.Dispose();
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public IActionResult ProductList() {
            ShoppingListDbContext context = new ShoppingListDbContext();
            ProductImageGetViewModel product_image_get_view_model = new ProductImageGetViewModel();
            List<Product> list_product = context.Products.Select(p => new Product() { ID = p.ID, Name = p.Name, Category = p.Category, Description = p.Description, Price = p.Price, ImageID = p.ImageID }).ToList();
            List<ProductImageGetViewModel> list_product_image = new List<ProductImageGetViewModel>();
            foreach (Product product in list_product) {
                Image image = context.Images.Single(i => i.ID == product.ImageID);
                list_product_image.Add(new ProductImageGetViewModel() { ProductID = product.ID, ProductName = product.Name, ProductCategory = product.Category, ProductDescription = product.Description, ProductPrice = product.Price, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) });
            }
            context.Dispose();
            return View(list_product_image);
        }
        [HttpGet]
        public IActionResult ProductCreate() {
            return View();
        }
        [HttpPost]
        public IActionResult ProductCreate(ProductImageSetViewModel product_image) {
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
            Product product = context.Products.Single(p => p.ID == id);
            Image image = context.Images.Single(i => i.ID == id);
            context.Dispose();
            return View(new ProductImageViewModel() { ProductID = product.ID, ProductName = product.Name, ProductCategory = product.Category, ProductDescription = product.Description, ProductPrice = product.Price, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) });
        }
        [HttpGet]
        public IActionResult ProductEdit(int id) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            Product product = context.Products.Single(p => p.ID == id);
            Image image = context.Images.Single(i => i.ID == id);
            context.Dispose();
            return View(new ProductImageViewModel() { ProductID = product.ID, ProductName = product.Name, ProductCategory = product.Category, ProductDescription = product.Description, ProductPrice = product.Price, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) });
        }
        [HttpPost]
        public IActionResult ProductEdit(ProductImageViewModel product_image_view_model) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            Product product = context.Products.Single(p => p.ID == product_image_view_model.ProductID);
            product.Name = product_image_view_model.ProductName;
            product.Category = product_image_view_model.ProductCategory;
            product.Description = product_image_view_model.ProductDescription;
            product.Price = product_image_view_model.ProductPrice;
            Image image = context.Images.Single(i => i.ID == product_image_view_model.ImageID);
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
            context.Products.Remove(context.Products.Single(p => p.ID == id));
            context.Images.Remove(context.Images.Single(i => i.ID == id));
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