using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class AccountRegisterViewModel {
		[Display(Name = "Email")]
		[StringLength(100)]
		public string Email { get; set; } = null!;
		[Display(Name = "Name")]
		[StringLength(100)]
		public string Name { get; set; } = null!;
		[Display(Name = "Surname")]
		[StringLength(100)]
		public string Surname { get; set; } = null!;
		[Display(Name = "Password")]
		[StringLength(100)]
		[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,100})", ErrorMessage = "Password must have at least 8 characters and contain one uppercase letter, one lowercase letter and one digit.")]
		public string Password { get; set; } = null!;
		[Display(Name = "Confirm Password")]
		[Compare("Password")]
		[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,100})", ErrorMessage = "Password must have at least 8 characters and contain one uppercase letter, one lowercase letter and one digit")]
		public string PasswordConfirm { get; set; } = null!;
	}
}