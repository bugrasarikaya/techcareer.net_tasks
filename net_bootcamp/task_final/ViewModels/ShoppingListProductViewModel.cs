using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ShoppingListProductViewModel {
		public int ShoppingProductID { get; set; }
		public string ShoppingProductStatus { get; set; } = null!;
		public List<SelectListItem> ShoppingProductStatuses { get; set; } = null!;
		public int ShoppingProductQuantity { get; set; }
		public double ShoppingProductTotalPrice { get; set; }
		public int ShoppingListID { get; set; }
		public string ShoppingListName { get; set; } = null!;
		public double ShoppingListTotalCost { get; set; }
		public string ShoppingListStatus { get; set; } = null!;
		public int ProductID { get; set; }
		public string ProductName { get; set; } = null!;
	}
}