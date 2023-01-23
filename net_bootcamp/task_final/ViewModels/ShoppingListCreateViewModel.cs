using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
    public class ShoppingListCreateViewModel {
        public int ID { get; set; }
		[StringLength(100)]
		public string Name { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public List<SelectListItem> AccountEmails { set; get; } = null!;
    }
}