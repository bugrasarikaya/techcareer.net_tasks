using System.ComponentModel.DataAnnotations;
namespace task_final.Models {
    public class Category {
        public int ID { get; set; }
		[StringLength(100)]
		public string Name { get; set; } = null!;
	}
}