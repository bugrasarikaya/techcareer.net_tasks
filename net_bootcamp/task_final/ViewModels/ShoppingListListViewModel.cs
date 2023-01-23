using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ShoppingListListViewModel {
		[Display(Name = "ID")]
		public int ID { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; } = null!;
		[Display(Name = "Account ID")]
		public int AccountID { get; set; }
		[Display(Name = "Account Email")]
		public string AccountEmail { get; set; } = null!;
		[Display(Name = "Total Cost ")]
		public double TotalCost { get; set; }
		[Display(Name = "Status")]
		public string Status { get; set; } = null!;
	}
}