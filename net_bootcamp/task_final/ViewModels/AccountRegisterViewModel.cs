using System.ComponentModel.DataAnnotations;
namespace task_final.ViewModels {
	public class AccountRegisterViewModel {
		[StringLength(100)]
		public string Email { get; set; } = null!;
		[StringLength(100)]
		public string Name { get; set; } = null!;
		[StringLength(100)]
		public string Surname { get; set; } = null!;
		[StringLength(100)]
		[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,100})", ErrorMessage = "Password must have at least 8 characters and contain one uppercase letter, one lowercase letter and one digit.")]
		public string Password { get; set; } = null!;
		[Compare("Password")]
		[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,100})", ErrorMessage = "Password must have at least 8 characters and contain one uppercase letter, one lowercase letter and one digit")]
		public string PasswordConfirm { get; set; } = null!;
	}
}