﻿using Microsoft.EntityFrameworkCore;
namespace task_final.Models {
	public class ShoppingListDbContext : DbContext {
		public DbSet<User> Users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options_builder) {
			options_builder.UseSqlServer("Server=.; Database=sl_db; Trusted_Connection=True; TrustServerCertificate=True");
		}
		protected override void OnModelCreating(ModelBuilder model_builder) {
			model_builder.ApplyConfiguration(new UserConfiguration());
		}
	}
}