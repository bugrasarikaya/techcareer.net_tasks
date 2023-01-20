namespace task_final.Models {
    public class Image {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public byte[] Binary { get; set; } = null!;
		//public Product Product { get; set; } = null!;
    }
}