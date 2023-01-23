using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ShoppingProductViewModel {
		[Display(Name = "Shoping Product ID")]
		public int ShoppingProductID { get; set; }
		[Display(Name = "Status")]
		public string ShoppingProductStatus { get; set; } = null!;
		public List<SelectListItem> ShoppingProductStatuses { get; set; } = null!;
		[Display(Name = "Quantity")]
		public int ShoppingProductQuantity { get; set; }
		[Display(Name = "Total Price")]
		public double ShoppingProductTotalPrice { get; set; }
		[Display(Name = "Product ID")]
		public int ProductID { get; set; }
		[Display(Name = "Name")]
		public string ProductName { get; set; } = null!;
	}
}