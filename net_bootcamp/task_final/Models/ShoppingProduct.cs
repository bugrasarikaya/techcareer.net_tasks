namespace task_final.Models {
	public class ShoppingProduct {
		public int ID { get; set; }
		public int ShoppingListID { get; set; }
		public int ProductID { get; set; }
        public string Status { get; set; } = null!;
		//public ShoppingList ShoppingList { get; set; } = null!;
		//public Product Product { get; set; } = null!;
    }
}