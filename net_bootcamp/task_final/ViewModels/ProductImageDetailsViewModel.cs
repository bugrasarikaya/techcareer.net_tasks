using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ProductImageDetailsViewModel {
		[Display(Name = "ID")]
		public int ProductID { get; set; }
		[Display(Name = "Name")]
		[StringLength(100)]
		public string ProductName { get; set; } = null!;
		[Display(Name = "Description")]
		[StringLength(100)]
		public string? ProductDescription { get; set; }
		[Display(Name = "Price")]
		public double ProductPrice { get; set; }
		[Display(Name = "Category")]
		[StringLength(100)]
		public string CategoryName { get; set; } = null!;
		[Display(Name = "Category ID")]
		public string CategoryID { get; set; } = null!;
		public List<SelectListItem> Categories { set; get; } = null!;
		[Display(Name = "Image ID")]
		public int ImageID { get; set; }
		[Display(Name = "Image Name")]
		[StringLength(100)]
		public string ImageName { get; set; } = null!;
		[Display(Name = "Image")]
		public string ImageSource { get; set; } = null!;
		public IFormFile? ImageBinary { get; set; }
	}
}