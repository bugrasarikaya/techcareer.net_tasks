using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_final.Models;
using task_final.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.Controllers {
	[Authorize(Roles = "User")]
	public class UserController : Controller {
		[HttpGet]
		public IActionResult Main() {
			return RedirectToAction("ShoppingListList");
		}
		public IActionResult AccountDetails() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == (int)HttpContext.Session.GetInt32("AccountID")!);
			context.Dispose();
			return View(account);
		}
		[HttpGet]
		public IActionResult AccountEdit(int account_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == account_id);
			context.Dispose();
			return View(new AccountViewModel() { ID = account.ID, Email = account.Email, Name = account.Name, Surname = account.Surname });
		}
		[HttpPost]
		public IActionResult AccountEdit(AccountViewModel account) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Accounts.Where(a => a.Email == account.Email).Count() > 0) {
				ModelState.AddModelError("Email", "Email is already exists.");
				context.Dispose();
				return View("AccountEdit", account);
			} else {
				Account account_original = context.Accounts.Single(a => a.ID == account.ID);
				account_original.Email = account.Email;
				account_original.Password = account.Password;
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("AccountDetails");
			}
		}
		[HttpGet]
		public IActionResult ShoppingListCreate() {
			return View();
		}
		[HttpPost]
		public IActionResult ShoppingListCreate(ShoppingList shopping_list) {
            ShoppingListDbContext context = new ShoppingListDbContext();
            if (context.ShoppingLists.Where(sl => sl.AccountID == (int)HttpContext.Session.GetInt32("AccountID")! && sl.Name == shopping_list.Name).Count() > 0) {
                ModelState.AddModelError("Name", "Shopping list name is already exists.");
                context.Dispose();
                return View("ShoppingListCreate", shopping_list);
            } else {
                shopping_list.AccountID = (int)HttpContext.Session.GetInt32("AccountID")!;
                shopping_list.Status = "Created";
                context.ShoppingLists.Add(shopping_list);
                context.SaveChanges();
                context.Dispose();
                return RedirectToAction("ShoppingListList");
            }
		}
		[HttpGet]
		public IActionResult ShoppingListDelete(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.ShoppingLists.Remove(context.ShoppingLists.Single(sl => sl.ID == shopping_list_id));
			context.ShoppingProducts.RemoveRange(context.ShoppingProducts.Where(spl => spl.ShoppingListID == shopping_list_id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListList");
		}
		[HttpGet]
		public IActionResult ShoppingListDetails(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingProductViewModel> list_shopping_list_product = new List<ShoppingProductViewModel>();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_id);
			Product product;
			List<ShoppingProduct> list_shopping_product = context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list_id).ToList();
			bool all_purchased = true;
			int total_quantity = 0;
			foreach (ShoppingProduct shopping_product in list_shopping_product) {
				if (all_purchased && shopping_product.Status != "Purchased") all_purchased = false;
				total_quantity += shopping_product.Quantity;
				product = context.Products.Single(p => p.ID == shopping_product.ProductID);
				list_shopping_list_product.Add(new ShoppingProductViewModel() { ShoppingProductID = shopping_product.ID, ShoppingProductStatus = shopping_product.Status, ProductID = shopping_product.ProductID, ProductName = product.Name, ShoppingProductQuantity = shopping_product.Quantity, ShoppingProductTotalPrice = shopping_product.TotalPrice });
			}
			context.Dispose();
			ViewData["AllPurchased"] = all_purchased;
			ViewData["ShoppingListStatus"] = shopping_list.Status;
			ViewData["ShoppingListTotalQuantiy"] = total_quantity;
			ViewData["ShoppingListTotalCost"] = shopping_list.TotalCost;
			ViewData["ShoppingListID"] = shopping_list_id;
			return View(list_shopping_list_product);
		}
		[HttpGet]
		public IActionResult ShoppingListEdit(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_id);
			context.Dispose();
			return View(shopping_list);
		}
		[HttpPost]
		public IActionResult ShoppingListEdit(ShoppingList shopping_list_edit) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.ShoppingLists.Where(sl => sl.AccountID == (int)HttpContext.Session.GetInt32("AccountID")! && sl.Name == shopping_list_edit.Name).Count() > 0) {
				ModelState.AddModelError("Name", "Shopping list name is already exists.");
				context.Dispose();
				return View("ShoppingListEdit", shopping_list_edit);
			} else {
				ShoppingList shopping_list_original = context.ShoppingLists.Single(sl => sl.ID == shopping_list_edit.ID);
				shopping_list_original.Name = shopping_list_edit.Name;
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("ShoppingListList");
			}
		}
		[HttpGet]
		public IActionResult ShoppingListList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingList> list_shopping_list = context.ShoppingLists.Where(sl => sl.AccountID == HttpContext.Session.GetInt32("AccountID")).ToList();
			return View(list_shopping_list);
		}
		[HttpGet]
		public IActionResult ShoppingListStart(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.ShoppingLists.Single(sl => sl.ID == shopping_list_id).Status = "On Shopping";
			foreach (ShoppingProduct shopping_product in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list_id)) shopping_product.Status = "Not Purchased";
			context.SaveChanges();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_list_id });
		}
		[HttpGet]
		public IActionResult ShoppingListComplete(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.ShoppingLists.Single(sl => sl.ID == shopping_list_id).Status = "Shopping Completed";
			context.SaveChanges();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_list_id });
		}
		[HttpGet]
		public IActionResult ShoppingProductAdd(int shopping_list_id, string category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			CategoryDropdownViewModel category_dropdown = new CategoryDropdownViewModel();
			category_dropdown.Categories = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) category_dropdown.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString(), Selected = category_id == category.ID.ToString() ? true : false });
			ViewData["CategoryDropdownViewModel"] = category_dropdown.Categories;
			ViewData["ShoppingListID"] = shopping_list_id;
			ProductImageGetViewModel product_image_get_view_model = new ProductImageGetViewModel();
			List<Product> list_product = context.Products.Where(p => p.CategoryID == ((category_id == "0" || category_id == null) ? p.CategoryID : int.Parse(category_id))).ToList();
			List<ProductImageGetViewModel> list_product_image = new List<ProductImageGetViewModel>();
			foreach (Product product in list_product) {
				Image image = context.Images.Single(i => i.ID == product.ImageID);
				list_product_image.Add(new ProductImageGetViewModel() { ProductID = product.ID, ProductName = product.Name, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, ProductDescription = product.Description, ProductPrice = product.Price, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) });
			}
			context.Dispose();
			return View(list_product_image);
		}
		[HttpPost]
		public IActionResult ShoppingProductAdd(ShoppingListProductIDViewModel shopping_list_product) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list_product.ShoppingListID && sp.ProductID == shopping_list_product.ProductID).ToList().Count == 0) {
				context.ShoppingProducts.Add(new ShoppingProduct() { ShoppingListID = shopping_list_product.ShoppingListID, ProductID = shopping_list_product.ProductID, Quantity = 1, TotalPrice = context.Products.Single(p => p.ID == shopping_list_product.ProductID).Price, Status = "Not Purchased" });
				context.SaveChanges();
				ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_list_product.ShoppingListID);
				shopping_list.TotalCost = 0;
				foreach (ShoppingProduct shopping_product in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) {
					shopping_list.TotalCost += shopping_product.TotalPrice;
					shopping_product.Status = "Not Purchased";
				}
				shopping_list.Status = "Updated";
			} else {
				ShoppingProduct shopping_product_1 = context.ShoppingProducts.Single(sp => sp.ShoppingListID == shopping_list_product.ShoppingListID && sp.ProductID == shopping_list_product.ProductID);
				shopping_product_1.Quantity++;
				shopping_product_1.TotalPrice = context.Products.Single(p => p.ID == shopping_list_product.ProductID).Price * shopping_product_1.Quantity;
				ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_list_product.ShoppingListID);
				shopping_list.TotalCost = 0;
				foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) {
					shopping_list.TotalCost += shopping_product_2.TotalPrice;
					shopping_product_2.Status = "Not Purchased";
				}
				shopping_list.Status = "Updated";
			}
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_list_product.ShoppingListID });
		}
		[HttpGet]
		public IActionResult ShoppingProductDelete(int shopping_product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product_1 = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_id);
			ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product_1.ShoppingListID);
			shopping_product_1.TotalPrice = 0;
			shopping_list.TotalCost = 0;
			foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) {
				shopping_list.TotalCost += shopping_product_2.TotalPrice;
				shopping_product_2.Status = "Not Purchased";
			}
			context.ShoppingProducts.Remove(shopping_product_1);
			shopping_list.Status = "Updated";
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product_1.ShoppingListID });
		}
		[HttpGet]
		public IActionResult ShoppingProductPurchase(int shopping_product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_id);
			if (context.ShoppingLists.Single(sl => sl.ID == shopping_product.ShoppingListID).Status == "On Shopping") {
				shopping_product.Status = "Purchased";
				context.SaveChanges();
			}
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product.ShoppingListID });
		}
		[HttpPost]
		public IActionResult ShoppingProductFilterCategory(CategoryDropdownViewModel category_dropdown) {
			return RedirectToAction("ShoppingProductAdd", new { shopping_list_id = category_dropdown.ShoppingListID, category_id = category_dropdown.CategoryID });
		}
		[HttpPost]
		public IActionResult ShoppingProductQuantity(ShoppingProductQuantityViewModel shopping_product_quantity) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_quantity.ShoppingProudctID);
			if (context.ShoppingLists.Single(sl => sl.ID == shopping_product.ShoppingListID).Status != "On Shopping") {
				shopping_product.Quantity = shopping_product_quantity.Quantity;
				shopping_product.TotalPrice = context.Products.Single(p => p.ID == shopping_product.ProductID).Price * shopping_product.Quantity;
				ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product.ShoppingListID);
				shopping_list.Status = "Updated";
				shopping_list.TotalCost = 0;
				foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) {
					shopping_list.TotalCost += shopping_product_2.TotalPrice;
					shopping_product_2.Status = "Not Purchased";
				}
				context.SaveChanges();
			}
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product.ShoppingListID });
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			HttpContext.Session.Clear();
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}