using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class CategoryDropdownViewModel {
		public int ShoppingListID { get; set; }
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
	}
}