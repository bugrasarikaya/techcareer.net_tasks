namespace task_final.Models {
    public class Product {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryID { get; set; }
        public int ImageID { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        //public Image Image { get; set; } = null!;
		//public ShoppingProduct? ShoppingProduct { get; set; }
	}
}