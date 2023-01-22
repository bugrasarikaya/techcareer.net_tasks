using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class ShoppingListEditViewModel {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string AccountID { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public List<SelectListItem> Accounts { set; get; } = null!;
        public string StatusName { get; set; } = null!;
        public List<SelectListItem> Statuses { set; get; } = null!;
    }
}