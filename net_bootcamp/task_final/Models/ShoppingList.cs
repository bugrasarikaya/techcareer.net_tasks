﻿namespace task_final.Models {
	public class ShoppingList {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public int AccountID { get; set; }
		public double TotalCost { get; set; }
		public string Status { get; set; } = null!;
		//public ShoppingProduct? ShoppingProduct { get; set; }
	}
}