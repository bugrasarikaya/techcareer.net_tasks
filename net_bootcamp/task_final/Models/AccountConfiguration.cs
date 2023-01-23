using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
	public class AccountConfiguration : IEntityTypeConfiguration<Account> {
		public void Configure(EntityTypeBuilder<Account> builder) {
			builder.ToTable("Accounts");
			builder.HasKey(u => u.ID);
			builder.Property(u => u.Email).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(u => u.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(u => u.Surname).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(u => u.Password).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
            builder.Property(u => u.Role).IsRequired(true).HasMaxLength(30).HasColumnType("varchar");
        }
	}
}