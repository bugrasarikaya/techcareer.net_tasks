using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ProductImageSetViewModel {
		public string ProductName { get; set; } = null!;
		public string? ProductDescription { get; set; }
		public double ProductPrice { get; set; }
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
		public IFormFile ImageBinary { get; set; } = null!;
	}
}