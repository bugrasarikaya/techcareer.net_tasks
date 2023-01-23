using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class ShoppingListEditViewModel {
        public int ID { get; set; }
		[Display(Name = "Name")]
		[StringLength(100)]
		public string Name { get; set; } = null!;
		[Display(Name = "Account Email")]
		[StringLength(100)]
		public string AccountEmail { get; set; } = null!;
        public List<SelectListItem> AccountEmails { set; get; } = null!;
		[Display(Name = "Status")]
		public string StatusName { get; set; } = null!;
        public List<SelectListItem> Statuses { set; get; } = null!;
    }
}