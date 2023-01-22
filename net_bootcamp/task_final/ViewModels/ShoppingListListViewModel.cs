namespace task_final.ViewModels {
	public class ShoppingListListViewModel {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public int AccountID { get; set; }
		public string AccountName { get; set; } = null!;
		public double TotalCost { get; set; }
		public string Status { get; set; } = null!;
	}
}