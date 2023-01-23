using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class CategoryDropdownViewModel {
		public int ShoppingListID { get; set; }
		[Display(Name = "Category Name:")]
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
	}
}