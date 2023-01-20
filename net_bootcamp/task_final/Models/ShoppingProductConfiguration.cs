using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
	public class ShoppingProductConfiguration : IEntityTypeConfiguration<ShoppingProduct> {
		public void Configure(EntityTypeBuilder<ShoppingProduct> builder) {
			builder.ToTable("ShoppingProducts");
			builder.HasKey(sp => sp.ID);
			builder.Property(sp => sp.ShoppingListID).IsRequired(true).HasColumnType("int");
			builder.Property(sp => sp.ProductID).IsRequired(true).HasColumnType("int");
			builder.Property(sp => sp.Status).IsRequired(true).HasMaxLength(20).HasColumnType("varchar");
			//builder.HasOne(sp => sp.ShoppingList).WithOne(sl => sl.ShoppingProduct).HasForeignKey<ShoppingProduct>(sp => sp.ShoppingListID);
			//builder.HasOne(sp => sp.Product).WithOne(p => p.ShoppingProduct).HasForeignKey<ShoppingProduct>(sp => sp.ProductID);
		}
	}
}