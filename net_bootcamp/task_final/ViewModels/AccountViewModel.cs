using Microsoft.AspNetCore.Mvc.Rendering;
namespace task_final.ViewModels {
	public class AccountViewModel {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string RoleName { get; set; } = null!;
		public List<SelectListItem> Roles { set; get; } = null!;
	}
}