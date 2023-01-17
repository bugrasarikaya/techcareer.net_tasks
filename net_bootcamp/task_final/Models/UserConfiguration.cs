using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace task_final.Models {
	public class UserConfiguration : IEntityTypeConfiguration<User> {
		public void Configure(EntityTypeBuilder<User> builder) {
			builder.ToTable("Users");
			builder.HasKey(u => u.id_user);
			builder.Property(u => u.username).IsRequired(true).HasMaxLength(25).HasColumnType("varchar");
			builder.Property(u => u.password).IsRequired(true).HasMaxLength(30).HasColumnType("varchar");
            builder.Property(u => u.role).IsRequired(false).HasMaxLength(20).HasColumnType("varchar");
        }
	}
}