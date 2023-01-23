using System.ComponentModel.DataAnnotations;
namespace task_final.Models {
	public class ShoppingList {
		[Display(Name = "ID")]
		public int ID { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; } = null!;
		[Display(Name = "Account ID")]
		public int AccountID { get; set; }
		[Display(Name = "Total Cost")]
		public double TotalCost { get; set; }
		[Display(Name = "Status")]
		public string Status { get; set; } = null!;
		//public ShoppingProduct? ShoppingProduct { get; set; }
	}
}