using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
    public class CategorytConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.ToTable("Categories");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Name).IsRequired(true).HasMaxLength(25).HasColumnType("varchar");
		}
    }
}