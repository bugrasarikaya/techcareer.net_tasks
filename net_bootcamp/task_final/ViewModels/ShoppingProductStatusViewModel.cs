using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ShoppingProductStatusViewModel {
		public int ShoppingProudctID { get; set; }
		public string StatusName { get; set; } = null!;
		public List<SelectListItem> Statuses { set; get; } = null!;
	}
}