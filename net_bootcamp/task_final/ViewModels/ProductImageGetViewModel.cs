using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ProductImageGetViewModel {
		[Display(Name = "Product ID")]
		public int ProductID { get; set; }
		[Display(Name = "Product Name")]
		public string ProductName { get; set; } = null!;
		[Display(Name = "Category Description")]
		public string? ProductDescription { get; set; }
		[Display(Name = "Product Price")]
		public double ProductPrice { get; set; }
		[Display(Name = "Category Name")]
		public string CategoryName { get; set; } = null!;
		[Display(Name = "Image")]
		public string ImageSource { get; set; } = null!;
    }
}