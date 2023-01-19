namespace task_final.ViewModels {
	public class ProductImageViewModel {
        public int ProductID { get; set; }
		public string ProductName { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public double? ProductPrice { get; set; }
		public int ImageID { get; set; }
        public string ImageName { get; set; } = null!;
        public string ImageSource { get; set; } = null!;
		public IFormFile ImageBinary { get; set; } = null!;
	}
}