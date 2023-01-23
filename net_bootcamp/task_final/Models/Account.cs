using System.ComponentModel.DataAnnotations;
namespace task_final.Models {
	public class Account {
		[Display(Name = "ID")]
		public int ID { get; set; }
		[Display(Name = "Email")]
		public string Email { get; set; } = null!;
		[Display(Name = "Name")]
		public string Name { get; set; } = null!;
		[Display(Name = "Surname")]
		public string Surname { get; set; } = null!;
		[Display(Name = "Password")]
		public string Password { get; set; } = null!;
		[Display(Name = "Role")]
		public string Role { get; set; } = null!;
	}
}