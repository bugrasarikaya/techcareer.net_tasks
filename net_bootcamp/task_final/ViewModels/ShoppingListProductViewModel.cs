namespace task_final.ViewModels {
	public class ShoppingListProductViewModel {
		public int ShoppingProductID { get; set; }
		public string ShoppingProductStatus { get; set; } = null!;
		public int ShoppingListID { get; set; }
		public string ShoppingListName { get; set; } = null!;
		public string ShoppingListStatus { get; set; } = null!;
		public int ProductID { get; set; }
		public string ProductName { get; set; } = null!;
		public string ProductCategory { get; set; } = null!;
		public string? ProductDescription { get; set; }
		public double? ProductPrice { get; set; }
	}
}