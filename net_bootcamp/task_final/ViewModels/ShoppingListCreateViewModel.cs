using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
    public class ShoppingListCreateViewModel {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string AccountID { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public List<SelectListItem> Accounts { set; get; } = null!;
    }
}