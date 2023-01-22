using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
	public class ShoppingListConfiguration: IEntityTypeConfiguration<ShoppingList> {
		public void Configure(EntityTypeBuilder<ShoppingList> builder) {
			builder.ToTable("ShoppingLists");
			builder.HasKey(sl => sl.ID);
			builder.Property(sl => sl.Name).IsRequired(true).HasMaxLength(25).HasColumnType("varchar");
			builder.Property(sl => sl.AccountID).IsRequired(true).HasColumnType("int");
			builder.Property(sl => sl.TotalCost).IsRequired(true).HasColumnType("float");
            builder.Property(sl => sl.Status).IsRequired(true).HasMaxLength(20).HasColumnType("varchar");
			//builder.HasOne(sl => sl.ShoppingProduct).WithOne(sp => sp.ShoppingList).HasForeignKey<ShoppingProduct>(sp => sp.ShoppingListID);
		}
	}
}