using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ProductImageDetailsViewModel {
        public int ProductID { get; set; }
		public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public double ProductPrice { get; set; }
		public string CategoryName { get; set; } = null!;
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
		public int ImageID { get; set; }
        public string ImageName { get; set; } = null!;
        public string ImageSource { get; set; } = null!;
		public IFormFile? ImageBinary { get; set; }
	}
}