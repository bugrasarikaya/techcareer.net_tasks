using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ProductImageSetViewModel {
		[StringLength(100)]
		public string ProductName { get; set; } = null!;
		[StringLength(100)]
		public string? ProductDescription { get; set; }
		public double ProductPrice { get; set; }
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
		public IFormFile ImageBinary { get; set; } = null!;
	}
}