using System.ComponentModel.DataAnnotations;
namespace task_final.Models {
	public class ShoppingList {
		public int ID { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; } = null!;
		public int AccountID { get; set; }
		public double TotalCost { get; set; }
		public string Status { get; set; } = null!;
		//public ShoppingProduct? ShoppingProduct { get; set; }
	}
}