using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
    public class ImageConfiguration : IEntityTypeConfiguration<Image> {
        public void Configure(EntityTypeBuilder<Image> builder) {
            builder.ToTable("Images");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
            builder.Property(i => i.Binary).IsRequired(true).HasColumnType("varbinary(max)");
            //builder.HasOne(i => i.Product).WithOne(p => p.Image).HasForeignKey<Product>(p => p.ImageID);
        }
    }
}