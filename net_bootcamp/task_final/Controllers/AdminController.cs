using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using task_final.Models;
using task_final.ViewModels;
namespace task_final.Controllers {
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller {
		[HttpGet]
		public IActionResult Main() {
			return RedirectToAction("ShoppingListList");
		}
		[HttpGet]
		public IActionResult AccountCreate() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
				new SelectListItem { Text = "Admin", Value ="Admin" },
				new SelectListItem { Text = "User", Value ="User" },
			};
			context.Dispose();
			return View(new AccountViewModel() { Roles = list_select_list_item });
		}
		[HttpPost]
		public IActionResult AccountCreate(AccountViewModel account) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Accounts.Where(a => a.Email == account.Email).Count() > 0) {
				ModelState.AddModelError("Email", "Email is already exists.");
				context.Dispose();
				return View("AccountCreate", account);
			} else {
				context.Accounts.Add(new Account() { Email = account.Email, Name = account.Name, Surname = account.Surname, Password = account.Password, Role = account.RoleName });
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("AccountList");
			}
		}
		[HttpGet]
		public IActionResult AccountDetails(int account_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == account_id);
			context.Dispose();
			return View(account);
		}
		[HttpGet]
		public IActionResult AccountDelete(int account_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Remove(context.Accounts.Single(a => a.ID == account_id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("AccountList");
		}
		[HttpGet]
		public IActionResult AccountEdit(int account_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == account_id);
			List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
				new SelectListItem { Text = "Admin", Value ="Admin" },
				new SelectListItem { Text = "User", Value ="User" }
			};
			foreach (SelectListItem select_list_item in list_select_list_item) select_list_item.Selected = (select_list_item.Text == account.Role) ? true : false;
			context.Dispose();
			return View(new AccountViewModel() { ID = account.ID, Email = account.Email, Name = account.Name, Surname = account.Surname, Password = account.Password, Roles = list_select_list_item });
		}
		[HttpPost]
		public IActionResult AccountEdit(AccountViewModel account) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Accounts.Where(a => a.ID != account.ID && a.Email == account.Email).Count() > 0) {
				ModelState.AddModelError("Email", "Email is already exists.");
				context.Dispose();
				List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
					new SelectListItem { Text = "Admin", Value ="Admin" },
					new SelectListItem { Text = "User", Value ="User" }
				};
				foreach (SelectListItem select_list_item in list_select_list_item) select_list_item.Selected = (select_list_item.Text == account.RoleName) ? true : false;
				account.Roles = list_select_list_item;
				return View("AccountEdit", account);
			} else {
				Account account_original = context.Accounts.Single(a => a.ID == account.ID);
				account_original.Email = account.Email;
				account_original.Password = account.Password;
				account_original.Role = account.RoleName;
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("AccountList");
			}
		}
		[HttpGet]
		public IActionResult AccountList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<Account> list_account = context.Accounts.Select(a => new Account() { ID = a.ID, Email = a.Email, Password = a.Password, Role = a.Role }).ToList();
			context.Dispose();
			return View(list_account);
		}
		[HttpGet]
		public IActionResult CategoryCreate() {
			return View();
		}
		[HttpPost]
		public IActionResult CategoryCreate(Category category) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Categories.Where(c => c.Name == category.Name).Count() > 0) {
				ModelState.AddModelError("Name", "Category name is already exists.");
				context.Dispose();
				return View("CategoryCreate", category);
			} else {
				context.Categories.Add(category);
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("CategoryList");
			}
		}
		[HttpGet]
		public IActionResult CategoryDelete(int category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Remove(context.Categories.Single(c => c.ID == category_id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("CategoryList");
		}
		[HttpGet]
		public IActionResult CategoryEdit(int category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Category category = context.Categories.Single(a => a.ID == category_id);
			context.Dispose();
			return View(category);
		}
		[HttpPost]
		public IActionResult CategoryEdit(Category category) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Categories.Where(c => c.Name == category.Name).Count() > 0) {
				ModelState.AddModelError("Name", "Category name is already exists.");
				context.Dispose();
				return View("CategoryEdit", category);
			} else {
				Category category_original = context.Categories.Single(a => a.ID == category.ID);
				category_original.Name = category.Name;
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("CategoryList");
			}
		}
		[HttpGet]
		public IActionResult CategoryList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<Category> list_category = context.Categories.Select(c => new Category() { ID = c.ID, Name = c.Name }).ToList();
			context.Dispose();
			return View(list_category);
		}
		[HttpGet]
		public IActionResult ProductCreate() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ProductImageSetViewModel product_image = new ProductImageSetViewModel();
			product_image.Categories = new List<SelectListItem>();
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) product_image.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
			return View(product_image);
		}
		[HttpPost]
		public IActionResult ProductCreate(ProductImageSetViewModel product_image) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Products.Where(p => p.Name == product_image.ProductName).Count() > 0) {
				ModelState.AddModelError("ProductName", "Product name is already exists.");
				product_image.Categories = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
				foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) product_image.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
				context.Dispose();
				return View("ProductCreate", product_image);
			} else {
				BinaryReader binary_reader = new BinaryReader(product_image.ImageBinary.OpenReadStream());
				Image image = new Image() { Name = product_image.ProductName, Binary = binary_reader.ReadBytes((int)product_image.ImageBinary.Length) };
				context.Images.Add(image);
				context.SaveChanges();
				Product product = new Product() { Name = product_image.ProductName, CategoryID = context.Categories.Single(c => c.ID == int.Parse(product_image.CategoryID)).ID, ImageID = image.ID, Description = product_image.ProductDescription, Price = product_image.ProductPrice };
				context.Products.Add(product);
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("ProductList");
			}
		}
		[HttpGet]
		public IActionResult ProductDelete(int product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Images.Remove(context.Images.Single(i => i.ID == context.Products.Single(p => p.ID == product_id).ImageID));
			context.ShoppingProducts.RemoveRange(context.ShoppingProducts.Where(spl => spl.ProductID == product_id));
			context.Products.Remove(context.Products.Single(p => p.ID == product_id));
			foreach (ShoppingList shopping_list in context.ShoppingLists.Select(sl => sl).ToList()) {
				shopping_list.TotalCost = 0;
				foreach (ShoppingProduct shopping_product in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product.TotalPrice;
			}
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ProductList");
		}
		[HttpGet]
		public IActionResult ProductDetails(int product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Product product = context.Products.Single(p => p.ID == product_id);
			Image image = context.Images.Single(i => i.ID == product.ImageID);
			ProductImageDetailsViewModel product_image = new ProductImageDetailsViewModel() { ProductID = product.ID, ProductName = product.Name, ProductDescription = product.Description, ProductPrice = product.Price, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) };
			context.Dispose();
			return View(product_image);
		}
		[HttpGet]
		public IActionResult ProductEdit(int product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Product product = context.Products.Single(p => p.ID == product_id);
			ProductImageDetailsViewModel product_image = new ProductImageDetailsViewModel();
			List<SelectListItem> list_select_list_item = new List<SelectListItem>();
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) list_select_list_item.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString(), Selected = (product.CategoryID == category.ID) ? true : false });
			Image image = context.Images.Single(i => i.ID == product_id);
			product_image = new ProductImageDetailsViewModel() { ProductID = product.ID, ProductName = product.Name, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, Categories = list_select_list_item, ProductDescription = product.Description, ProductPrice = product.Price, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) };
			context.Dispose();
			return View(product_image);
		}
		[HttpPost]
		public IActionResult ProductEdit(ProductImageDetailsViewModel product_image_view_model) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.Products.Where(p => p.Name == product_image_view_model.ProductName).Count() > 0) {
				ModelState.AddModelError("ProductName", "Product name is already exists.");
				product_image_view_model.Categories = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
				foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) product_image_view_model.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
				context.Dispose();
				return View("ProductEdit", product_image_view_model);
			} else {
				Product product = context.Products.Single(p => p.ID == product_image_view_model.ProductID);
				product.Name = product_image_view_model.ProductName;
				product.CategoryID = context.Categories.Single(c => c.Name == product_image_view_model.CategoryName).ID;
				product.Description = product_image_view_model.ProductDescription;
				product.Price = product_image_view_model.ProductPrice;
				Image image = context.Images.Single(i => i.ID == product_image_view_model.ImageID);
				image.Name = product_image_view_model.ProductName;
				if (product_image_view_model.ImageBinary != null) {
					BinaryReader binary_reader = new BinaryReader(product_image_view_model.ImageBinary.OpenReadStream());
					image.Binary = binary_reader.ReadBytes((int)product_image_view_model.ImageBinary.Length);
				}
				foreach (ShoppingProduct shopping_product_1 in context.ShoppingProducts.Where(sp => sp.ProductID == product_image_view_model.ProductID).ToList()) {
					shopping_product_1.TotalPrice = product.Price * shopping_product_1.Quantity;
					ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product_1.ShoppingListID);
					shopping_list.TotalCost = 0;
					foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
				}
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("ProductList");
			}
		}
		[HttpGet]
		public IActionResult ProductList(string category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			CategoryDropdownViewModel category_dropdown = new CategoryDropdownViewModel();
			category_dropdown.Categories = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) category_dropdown.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString(), Selected = category_id == category.ID.ToString() ? true : false });
			ViewData["CategoryDropdownViewModel"] = category_dropdown.Categories;
			List<Product> list_product = context.Products.Where(p => p.CategoryID == ((category_id == "0" || category_id == null) ? p.CategoryID : int.Parse(category_id))).ToList();
			List<ProductImageGetViewModel> list_product_image = new List<ProductImageGetViewModel>();
			foreach (Product product in list_product) {
				Image image = context.Images.Single(i => i.ID == product.ImageID);
				list_product_image.Add(new ProductImageGetViewModel() { ProductID = product.ID, ProductName = product.Name, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, ProductDescription = product.Description, ProductPrice = product.Price, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) });
			}
			context.Dispose();
			return View(list_product_image);
		}
		[HttpGet]
		public IActionResult ShoppingListCreate() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<SelectListItem> list_select_list_item = new List<SelectListItem>();
			foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item.Add(new SelectListItem { Text = account.Email, Value = account.Email });
			context.Dispose();
			return View(new ShoppingListCreateViewModel() { AccountEmails = list_select_list_item });
		}
		[HttpPost]
		public IActionResult ShoppingListCreate(ShoppingListCreateViewModel shopping_list) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			if (context.ShoppingLists.Where(sl => sl.AccountID == context.Accounts.Single(a => a.Email == shopping_list.AccountEmail).ID && sl.Name == shopping_list.Name).Count() > 0) {
				ModelState.AddModelError("Name", "Shopping list name is already exists.");
				List<SelectListItem> list_select_list_item = new List<SelectListItem>();
				foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item.Add(new SelectListItem { Text = account.Email, Value = account.Email });
				context.Dispose();
				shopping_list.AccountEmails = list_select_list_item;
				return View("ShoppingListCreate", shopping_list);
			} else {
				context.ShoppingLists.Add(new ShoppingList() { Name = shopping_list.Name, AccountID = context.Accounts.Single(a => a.Email == shopping_list.AccountEmail).ID, Status = "Created" });
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
			ViewData["ShoppingListID"] = shopping_list_id;
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingProductViewModel> list_shopping_list_product = new List<ShoppingProductViewModel>();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_id);
			Product product;
			List<ShoppingProduct> list_shopping_product = context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list_id).ToList();
			List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
				new SelectListItem () {Text = "Not Purchased", Value = "Not Purchased"},
				new SelectListItem () {Text = "Purchased", Value = "Purchased"},
			};
			int total_quantity = 0;
			foreach (ShoppingProduct shopping_product in list_shopping_product) {
				total_quantity += shopping_product.Quantity;
				product = context.Products.Single(p => p.ID == shopping_product.ProductID);
				foreach (SelectListItem select_list_item in list_select_list_item) select_list_item.Selected = (select_list_item.Text == shopping_product.Status) ? true : false;
				list_shopping_list_product.Add(new ShoppingProductViewModel() { ShoppingProductID = shopping_product.ID, ShoppingProductStatus = shopping_product.Status, ShoppingProductStatuses = list_select_list_item, ProductID = shopping_product.ProductID, ProductName = product.Name, ShoppingProductQuantity = shopping_product.Quantity, ShoppingProductTotalPrice = shopping_product.TotalPrice });
			}
			ViewData["ShoppingListStatus"] = shopping_list.Status;
			ViewData["ShoppingListTotalQuantiy"] = total_quantity;
			ViewData["ShoppingListTotalCost"] = shopping_list.TotalCost;
			context.Dispose();
			return View(list_shopping_list_product);
		}
		[HttpGet]
		public IActionResult ShoppingListEdit(int shopping_list_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_id);
			List<SelectListItem> list_select_list_item_account = new List<SelectListItem>();
			foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item_account.Add(new SelectListItem { Text = account.Email, Value = account.Email });
			foreach (SelectListItem select_list_item in list_select_list_item_account) select_list_item.Selected = (select_list_item.Value == context.Accounts.Single(a => a.ID == shopping_list.AccountID).Email) ? true : false;
			List<SelectListItem> list_select_list_item_status = new List<SelectListItem>() {
				new SelectListItem () {Text = "Updated", Value = "Updated"},
				new SelectListItem () {Text = "On Shopping", Value = "On Shopping"},
				new SelectListItem () {Text = "Shopping Completed", Value = "Shopping Completed"}
			};
			foreach (SelectListItem select_list_item in list_select_list_item_status) select_list_item.Selected = (select_list_item.Text == shopping_list.Status) ? true : false;
			context.Dispose();
			return View(new ShoppingListEditViewModel() { ID = shopping_list_id, Name = shopping_list.Name, AccountEmails = list_select_list_item_account, StatusName = shopping_list.Status, Statuses = list_select_list_item_status });
		}
		[HttpPost]
		public IActionResult ShoppingListEdit(ShoppingListEditViewModel shopping_list_edit) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			//if (context.Accounts.Where(a => a.ID != account.ID && a.Email == account.Email).Count() > 0) {
			if (context.ShoppingLists.Where(sl => sl.ID != shopping_list_edit.ID && sl.AccountID == context.Accounts.Single(a => a.Email == shopping_list_edit.AccountEmail).ID && sl.Name == shopping_list_edit.Name).Count() > 0) {
				ModelState.AddModelError("Name", "Shopping list name is already exists.");
				List<SelectListItem> list_select_list_item_account_emails = new List<SelectListItem>();
				foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item_account_emails.Add(new SelectListItem { Text = account.Email, Value = account.Email });
				foreach (SelectListItem select_list_item in list_select_list_item_account_emails) select_list_item.Selected = (select_list_item.Value == shopping_list_edit.AccountEmail) ? true : false;
				List<SelectListItem> list_select_list_item_statuses = new List<SelectListItem>() {
					new SelectListItem () {Text = "Updated", Value = "Updated"},
					new SelectListItem () {Text = "On Shopping", Value = "On Shopping"},
					new SelectListItem () {Text = "Shopping Completed", Value = "Shopping Completed" }
				};
				foreach (SelectListItem select_list_item in list_select_list_item_statuses) select_list_item.Selected = (select_list_item.Text == shopping_list_edit.StatusName) ? true : false;
				context.Dispose();
				shopping_list_edit.AccountEmails = list_select_list_item_account_emails;
				shopping_list_edit.Statuses = list_select_list_item_statuses;
				return View("ShoppingListEdit", shopping_list_edit);
			} else {
				ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_edit.ID);
				shopping_list.Name = shopping_list_edit.Name;
				shopping_list.AccountID = context.Accounts.Single(a => a.Email == shopping_list_edit.AccountEmail).ID;
				shopping_list.Status = shopping_list_edit.StatusName;
				context.SaveChanges();
				context.Dispose();
				return RedirectToAction("ShoppingListList");
			}
		}
		[HttpGet]
		public IActionResult ShoppingListList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingList> list_shopping_list = context.ShoppingLists.Select(sl => sl).ToList();
			List<ShoppingListListViewModel> list_shopping_list_list = new List<ShoppingListListViewModel>();
			foreach (ShoppingList shopping_list in list_shopping_list) list_shopping_list_list.Add(new ShoppingListListViewModel() { ID = shopping_list.ID, Name = shopping_list.Name, AccountID = shopping_list.AccountID, AccountEmail = context.Accounts.Single(a => a.ID == shopping_list.AccountID).Email, Status = shopping_list.Status, TotalCost = shopping_list.TotalCost });
			return View(list_shopping_list_list);
		}
		[HttpPost]
		public IActionResult ProductFilterCategory(CategoryDropdownViewModel category_dropdown) {
			return RedirectToAction("ProductList", new { category_id = category_dropdown.CategoryID });
		}
		[HttpGet]
		public IActionResult ShoppingProductAdd(int shopping_list_id, string category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			CategoryDropdownViewModel category_dropdown = new CategoryDropdownViewModel();
			category_dropdown.Categories = new List<SelectListItem> { new SelectListItem { Text = "All", Value = "0" } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) category_dropdown.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString(), Selected = category_id == category.ID.ToString() ? true : false });
			ViewData["CategoryDropdownViewModel"] = category_dropdown.Categories;
			ViewData["ShoppingListID"] = shopping_list_id;
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
				foreach (ShoppingProduct shopping_product in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product.TotalPrice;
				shopping_list.Status = "Updated";
			} else {
				ShoppingProduct shopping_product_1 = context.ShoppingProducts.Single(sp => sp.ShoppingListID == shopping_list_product.ShoppingListID && sp.ProductID == shopping_list_product.ProductID);
				shopping_product_1.Quantity++;
				shopping_product_1.TotalPrice = context.Products.Single(p => p.ID == shopping_list_product.ProductID).Price * shopping_product_1.Quantity;
				ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_list_product.ShoppingListID);
				shopping_list.TotalCost = 0;
				foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
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
			foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
			context.ShoppingProducts.Remove(shopping_product_1);
			shopping_list.Status = "Updated";
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product_1.ShoppingListID });
		}
		[HttpGet]
		public IActionResult ShoppingProductDetails(int shopping_product_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(p => p.ID == shopping_product_id);
			Image image = context.Images.Single(i => i.ID == context.Products.Single(p => p.ID == shopping_product.ProductID).ImageID);
			ShoppingProductImageDetailsViewModel shopping_product_image = new ShoppingProductImageDetailsViewModel() { ShoppingProductID = shopping_product.ID, ProductID = shopping_product.ProductID, ProductName = context.Products.Single(p => p.ID == shopping_product.ProductID).Name, ProductDescription = context.Products.Single(p => p.ID == shopping_product.ProductID).Description, ProductPrice = context.Products.Single(p => p.ID == shopping_product.ProductID).Price, CategoryID = context.Products.Single(p => p.ID == shopping_product.ProductID).CategoryID, CategoryName = context.Categories.Single(c => c.ID == context.Products.Single(p => p.ID == shopping_product.ProductID).CategoryID).Name, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) };
			context.Dispose();
			return View(shopping_product_image);
		}
		[HttpPost]
		public IActionResult ShoppingProductFilterCategory(CategoryDropdownViewModel category_dropdown) {
			return RedirectToAction("ShoppingProductAdd", new { shopping_list_id = category_dropdown.ShoppingListID, category_id = category_dropdown.CategoryID });
		}
		[HttpPost]
		public IActionResult ShoppingProductQuantity(ShoppingProductQuantityViewModel shopping_product_quantity) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_quantity.ShoppingProudctID);
			shopping_product.Quantity = shopping_product_quantity.Quantity;
			shopping_product.TotalPrice = context.Products.Single(p => p.ID == shopping_product.ProductID).Price * shopping_product.Quantity;
			ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product.ShoppingListID);
			shopping_list.TotalCost = 0;
			foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product.ShoppingListID });
		}
		[HttpPost]
		public IActionResult ShoppingProductStatus(ShoppingProductStatusViewModel shopping_product_status) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_status.ShoppingProudctID);
			shopping_product.Status = shopping_product_status.StatusName;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { shopping_list_id = shopping_product.ShoppingListID });
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}