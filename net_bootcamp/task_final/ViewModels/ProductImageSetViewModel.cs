namespace task_final.ViewModels {
	public class ProductImageSetViewModel {
		public string ProductName { get; set; } = null!;
		public string ProductCategory { get; set; } = null!;
		public string? ProductDescription { get; set; }
		public double? ProductPrice { get; set; }
		public IFormFile ImageBinary { get; set; } = null!;
	}
}