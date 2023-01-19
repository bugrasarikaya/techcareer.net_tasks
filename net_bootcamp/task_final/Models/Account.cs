namespace task_final.Models {
	public class Account {
		public int ID { get; set; }
		public string AccountName { get; set; } = null!;
		public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
	}
}