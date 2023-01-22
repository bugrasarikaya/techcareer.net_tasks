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
			return View();
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
			context.Accounts.Add(new Account() { Name = account.Name, Password = account.Password, Role = account.RoleName });
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
		public IActionResult AccountDelete(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Remove(context.Accounts.Single(a => a.ID == id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("AccountList");
		}
		[HttpGet]
		public IActionResult AccountEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account = context.Accounts.Single(a => a.ID == id);
			List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
				new SelectListItem { Text = "Admin", Value ="Admin" },
				new SelectListItem { Text = "User", Value ="User" },
			};
			foreach (SelectListItem select_list_item in list_select_list_item) select_list_item.Selected = (select_list_item.Text == account.Role) ? true : false;
			context.Dispose();
			return View(new AccountViewModel() { ID = account.ID, Name = account.Name, Password = account.Password, Roles = list_select_list_item });
		}
		[HttpPost]
		public IActionResult AccountEdit(AccountViewModel account) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Account account_original = context.Accounts.Single(a => a.ID == account.ID);
			account_original.Name = account.Name;
			account_original.Password = account.Password;
			account_original.Role = account.RoleName;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("AccountList");
		}
		[HttpGet]
		public IActionResult AccountList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<Account> list_account = context.Accounts.Select(a => new Account() { ID = a.ID, Name = a.Name, Password = a.Password, Role = a.Role }).ToList();
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
			context.Categories.Add(category);
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("CategoryList");
		}
		[HttpGet]
		public IActionResult CategorytDelete(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Remove(context.Categories.Single(c => c.ID == id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("CategoryList");
		}
		[HttpGet]
		public IActionResult CategoryEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Category category = context.Categories.Single(a => a.ID == id);
			context.Dispose();
			return View(category);
		}
		[HttpPost]
		public IActionResult CategoryEdit(Category category) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Category category_original = context.Categories.Single(a => a.ID == category.ID);
			category_original.Name = category.Name;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("CategoryList");
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
			product_image.Categories = new List<SelectListItem> { new SelectListItem { Text = "", Value = null } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) product_image.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
			return View(product_image);
		}
		[HttpPost]
		public IActionResult ProductCreate(ProductImageSetViewModel product_image) {
			BinaryReader binary_reader = new BinaryReader(product_image.ImageBinary.OpenReadStream());
			Image image = new Image() { Name = product_image.ProductName, Binary = binary_reader.ReadBytes((int)product_image.ImageBinary.Length) };
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.Images.Add(image);
			context.SaveChanges();
			Product product = new Product() { Name = product_image.ProductName, CategoryID = context.Categories.Single(c => c.ID == int.Parse(product_image.CategoryID)).ID, ImageID = image.ID, Description = product_image.ProductDescription, Price = product_image.ProductPrice };
			context.Products.Add(product);
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
		public IActionResult ProductDetails(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Product product = context.Products.Single(p => p.ID == id);
			Image image = context.Images.Single(i => i.ID == product.ImageID);
			ProductImageDetailsViewModel product_image = new ProductImageDetailsViewModel() { ProductID = product.ID, ProductName = product.Name, ProductDescription = product.Description, ProductPrice = product.Price, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) };
			context.Dispose();
			return View(product_image);
		}
		[HttpGet]
		public IActionResult ProductEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			Product product = context.Products.Single(p => p.ID == id);
			ProductImageDetailsViewModel product_image = new ProductImageDetailsViewModel();
			List<SelectListItem> list_select_list_item = new List<SelectListItem>();
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) list_select_list_item.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString(), Selected = (product.CategoryID == category.ID) ? true : false });
			Image image = context.Images.Single(i => i.ID == id);
			product_image = new ProductImageDetailsViewModel() { ProductID = product.ID, ProductName = product.Name, CategoryName = context.Categories.Single(c => c.ID == product.CategoryID).Name, Categories = list_select_list_item, ProductDescription = product.Description, ProductPrice = product.Price, ImageID = image.ID, ImageName = image.Name, ImageSource = string.Format("data:.jpg; base64, {0}", Convert.ToBase64String(image.Binary)) };
			context.Dispose();
			return View(product_image);
		}
		[HttpPost]
		public IActionResult ProductEdit(ProductImageDetailsViewModel product_image_view_model) {
			ShoppingListDbContext context = new ShoppingListDbContext();
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
		[HttpGet]
		public IActionResult ProductList(string category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			CategoryDropdownViewModel category_dropdown = new CategoryDropdownViewModel();
			category_dropdown.Categories = new List<SelectListItem> { new SelectListItem { Text = "", Value = null } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) category_dropdown.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
			ViewData["CategoryDropdownViewModel"] = category_dropdown.Categories;
			ProductImageGetViewModel product_image_get_view_model = new ProductImageGetViewModel();
			List<Product> list_product = context.Products.Where(p => p.CategoryID == (category_id == null ? p.CategoryID : int.Parse(category_id))).ToList();
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
			foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item.Add(new SelectListItem { Text = account.Name, Value = account.ID.ToString() });
			context.Dispose();
			return View(new ShoppingListCreateViewModel() { Accounts = list_select_list_item });
		}
		[HttpPost]
		public IActionResult ShoppingListCreate(ShoppingList shopping_list) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			shopping_list.Status = "Created";
			context.ShoppingLists.Add(shopping_list);
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListList");
		}
		[HttpGet]
		public IActionResult ShoppingListDelete(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			context.ShoppingLists.Remove(context.ShoppingLists.Single(sl => sl.ID == id));
			context.ShoppingProducts.RemoveRange(context.ShoppingProducts.Where(spl => spl.ShoppingListID == id));
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListList");
		}
		[HttpGet]
		public IActionResult ShoppingListDetails(int id) {
			ViewData["ShoppingListID"] = id;
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingListProductViewModel> list_shopping_list_product = new List<ShoppingListProductViewModel>();
			ShoppingList shopping_list;
			Product product;
			List<ShoppingProduct> list_shopping_product = context.ShoppingProducts.Select(sp => sp).ToList();
			foreach (ShoppingProduct shopping_product in list_shopping_product) {
				shopping_list = context.ShoppingLists.Single(sl => sl.ID == id);
				product = context.Products.Single(p => p.ID == shopping_product.ProductID);
				list_shopping_list_product.Add(new ShoppingListProductViewModel() { ShoppingProductID = shopping_product.ID, ShoppingProductStatus = shopping_product.Status, ShoppingListID = id, ShoppingListName = shopping_list.Name, ShoppingListStatus = shopping_list.Status, ProductID = shopping_product.ProductID, ProductName = product.Name, ShoppingProductQuantity = shopping_product.Quantity, ShoppingProductTotalPrice = shopping_product.TotalPrice });
			}
			context.Dispose();
			return View(list_shopping_list_product);
		}
		[HttpGet]
		public IActionResult ShoppingListEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == id);
			List<SelectListItem> list_select_list_item_account = new List<SelectListItem>();
			foreach (Account account in context.Accounts.Select(sl => sl).ToList()) list_select_list_item_account.Add(new SelectListItem { Text = account.Name, Value = account.ID.ToString() });
			foreach (SelectListItem select_list_item in list_select_list_item_account) select_list_item.Selected = (select_list_item.Value == shopping_list.AccountID.ToString()) ? true : false;
			List<SelectListItem> list_select_list_item_status = new List<SelectListItem>() {
				new SelectListItem () {Text = "Updated", Value = "Updated"},
				new SelectListItem () {Text = "On Shopping", Value = "On Shopping"},
				new SelectListItem () {Text = "Shopping Completed", Value = "Shopping Completed"}
			};
			foreach (SelectListItem select_list_item in list_select_list_item_status) select_list_item.Selected = (select_list_item.Text == shopping_list.Status) ? true : false;
			context.Dispose();
			return View(new ShoppingListEditViewModel() { ID = id, Name = shopping_list.Name, Accounts = list_select_list_item_account, StatusName = shopping_list.Status, Statuses = list_select_list_item_status });
		}
		[HttpPost]
		public IActionResult ShoppingListEdit(ShoppingListEditViewModel shopping_list_edit) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingList shopping_list = context.ShoppingLists.Single(sl => sl.ID == shopping_list_edit.ID);
			shopping_list.Name = shopping_list_edit.Name;
			shopping_list.AccountID = int.Parse(shopping_list_edit.AccountID);
			shopping_list.Status = shopping_list_edit.StatusName;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListList");
		}
		//[HttpGet]
		//public IActionResult ShoppingListStart(int id) {
		//	ShoppingListDbContext context = new ShoppingListDbContext();
		//	context.ShoppingLists.Single(sl => sl.ID == id).Status = "OnShopping";
		//	return RedirectToAction("ShoppingListDetails", new { id = id });
		//}
		[HttpGet]
		public IActionResult ShoppingListList() {
			ShoppingListDbContext context = new ShoppingListDbContext();
			List<ShoppingList> list_shopping_list = context.ShoppingLists.Select(sl => sl).ToList();
			List<ShoppingListListViewModel> list_shopping_list_list = new List<ShoppingListListViewModel>();
			foreach (ShoppingList shopping_list in list_shopping_list) list_shopping_list_list.Add(new ShoppingListListViewModel() { ID = shopping_list.ID, Name = shopping_list.Name, AccountID = shopping_list.AccountID, AccountName = context.Accounts.Single(a => a.ID == shopping_list.AccountID).Name, Status = shopping_list.Status, TotalCost = shopping_list.TotalCost });
			return View(list_shopping_list_list);
		}
		[HttpPost]
		public IActionResult ProductFilterCategory(CategoryDropdownViewModel category_dropdown) {
			return RedirectToAction("ProductList", new { category_id = category_dropdown.CategoryID });
		}
		[HttpGet]
		public IActionResult ShoppingProductAdd(int id, string category_id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			CategoryDropdownViewModel category_dropdown = new CategoryDropdownViewModel();
			category_dropdown.Categories = new List<SelectListItem> { new SelectListItem { Text = "", Value = null } };
			foreach (Category category in context.Categories.Select(a => new Category() { ID = a.ID, Name = a.Name }).ToList()) category_dropdown.Categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
			ViewData["CategoryDropdownViewModel"] = category_dropdown.Categories;
			ViewData["ShoppingListID"] = id;
			ProductImageGetViewModel product_image_get_view_model = new ProductImageGetViewModel();
			List<Product> list_product = context.Products.Where(p => p.CategoryID == (category_id == null ? p.CategoryID : int.Parse(category_id))).ToList();
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
			return RedirectToAction("ShoppingListDetails", new { id = shopping_list_product.ShoppingListID });
		}
		[HttpGet]
		public IActionResult ShoppingProductDelete(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product_1 = context.ShoppingProducts.Single(sp => sp.ID == id);
			ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product_1.ShoppingListID);
			shopping_product_1.TotalPrice = 0;
			shopping_list.TotalCost = 0;
			foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
			context.ShoppingProducts.Remove(shopping_product_1);
			shopping_list.Status = "Updated";
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { id = shopping_product_1.ShoppingListID });
		}
		[HttpGet]
		public IActionResult ShoppingProductEdit(int id) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product = context.ShoppingProducts.Single(sp => sp.ID == id);
			List<SelectListItem> list_select_list_item = new List<SelectListItem>() {
				new SelectListItem () {Text = "Not Purchased", Value = "Not Purchased"},
				new SelectListItem () {Text = "Purchased", Value = "Purchased"},
			};
			foreach (SelectListItem select_list_item in list_select_list_item) select_list_item.Selected = (select_list_item.Text == shopping_product.Status) ? true : false;
			context.Dispose();
			return View(new ShoppingProductEditViewModel() { ID = id, Quantity = shopping_product.Quantity, StatusName = shopping_product.Status, Statuses = list_select_list_item });
		}
		[HttpPost]
		public IActionResult ShoppingProductEdit(ShoppingProductEditViewModel shopping_product_edit) {
			ShoppingListDbContext context = new ShoppingListDbContext();
			ShoppingProduct shopping_product_1 = context.ShoppingProducts.Single(sp => sp.ID == shopping_product_edit.ID);
			shopping_product_1.Quantity = shopping_product_edit.Quantity;
			shopping_product_1.Status = shopping_product_edit.StatusName;
			shopping_product_1.TotalPrice = context.Products.Single(p => p.ID == shopping_product_1.ProductID).Price * shopping_product_1.Quantity;
			ShoppingList shopping_list = context.ShoppingLists.Single(sp => sp.ID == shopping_product_1.ShoppingListID);
			shopping_list.TotalCost = 0;
			foreach (ShoppingProduct shopping_product_2 in context.ShoppingProducts.Where(sp => sp.ShoppingListID == shopping_list.ID).ToList()) shopping_list.TotalCost += shopping_product_2.TotalPrice;
			context.SaveChanges();
			context.Dispose();
			return RedirectToAction("ShoppingListDetails", new { id = shopping_product_1.ShoppingListID });
		}
		[HttpPost]
		public IActionResult ShoppingProductFilterCategory(CategoryDropdownViewModel category_dropdown) {
			return RedirectToAction("ShoppingProductAdd", new { id = category_dropdown.ShoppingListID, category_id = category_dropdown.CategoryID });
		}
		[HttpGet]
		public async Task<IActionResult> LogOut() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Main", "Login");
		}
	}
}