using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
    public class ProductConfiguration : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.ToTable("Products");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(25).HasColumnType("varchar");
            builder.Property(p => p.CategoryID).IsRequired(true).HasColumnType("int");
            builder.Property(p => p.ImageID).IsRequired(true).HasColumnType("int");
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(20).HasColumnType("varchar");
            builder.Property(p => p.Price).IsRequired(true).HasColumnType("float");
            //builder.HasOne(p => p.Image).WithOne(i => i.Product).HasForeignKey<Product>(b => b.ImageID);
            //builder.HasOne(p => p.ShoppingProduct).WithOne(sp => sp.Product).HasForeignKey<ShoppingProduct>(sp => sp.ProductID);
		}
    }
}