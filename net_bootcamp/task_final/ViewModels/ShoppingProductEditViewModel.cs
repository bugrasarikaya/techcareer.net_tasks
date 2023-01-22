using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ShoppingProductEditViewModel {
		public int ID { get; set; }
		public int Quantity { get; set; }
		public string StatusName { get; set; } = null!;
		public List<SelectListItem> Statuses { set; get; } = null!;
	}
}