namespace task_final.ViewModels {
	public class ProductImageGetViewModel {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public double? ProductPrice { get; set; }
        public string ImageSource { get; set; } = null!;
    }
}