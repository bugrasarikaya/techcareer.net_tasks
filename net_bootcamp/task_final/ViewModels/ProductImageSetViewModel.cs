using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ProductImageSetViewModel {
		[Display(Name = "Name")]
		[StringLength(100)]
		public string ProductName { get; set; } = null!;
		[Display(Name = "Description")]
		[StringLength(100)]
		public string? ProductDescription { get; set; }
		[Display(Name = "Price")]
		public double ProductPrice { get; set; }
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
		[Display(Name = "Image")]
		public IFormFile ImageBinary { get; set; } = null!;
	}
}