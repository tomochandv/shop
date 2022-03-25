using Microsoft.EntityFrameworkCore;
using shop_model;

namespace shop_admin_api
{
	public class ShopDbContext : DbContext
	{

		public ShopDbContext(DbContextOptions<ShopDbContext> options)
		: base(options)
		{
		}


		public DbSet<UserInfo> Users { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Category> Categorys { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductOption> ProductOptions { get; set; }
		public DbSet<ProductOptionValues> ProductOptionValues { get; set; }
		public DbSet<ProductDetailImage> ProductDetailImages { get; set; }
		public DbSet<Cart> Cartes { get; set; }
		public DbSet<CartOptionValue> CartOptionValues { get; set; }

		public DbSet<Order> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserInfo>().HasOne<Region>().WithMany().HasForeignKey(p => p.RegionIdx);
			modelBuilder.Entity<UserInfo>().Property(u => u.Status).HasDefaultValue(Status.NotAuth);
			modelBuilder.Entity<UserInfo>().Property(u => u.Type).HasDefaultValue(UserType.User);
			modelBuilder.Entity<UserInfo>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");

			modelBuilder.Entity<Product>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			modelBuilder.Entity<Category>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			modelBuilder.Entity<ProductOption>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			modelBuilder.Entity<ProductDetailImage>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			modelBuilder.Entity<ProductOptionValues>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");

			modelBuilder.Entity<Cart>().HasOne<UserInfo>().WithMany().HasForeignKey(p => p.Useridx);
			modelBuilder.Entity<Cart>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			
			modelBuilder.Entity<CartOptionValue>().HasOne<ProductOptionValues>().WithMany().HasForeignKey(p => p.ProductOptionValues);

			modelBuilder.Entity<Region>().HasIndex(p => new { p.RegionCode }).IsUnique();
			modelBuilder.Entity<UserInfo>().HasIndex(p => new { p.Email }).IsUnique();

			modelBuilder.Entity<Order>().HasOne<UserInfo>().WithMany().HasForeignKey(p => p.Useridx);
			modelBuilder.Entity<Order>().Property(u => u.Regdate).HasDefaultValueSql("NOW()");
			modelBuilder.Entity<OrderProducts>().HasOne<Order>().WithMany().HasForeignKey(p => p.OrderIdx);
			modelBuilder.Entity<OrderProducts>().HasOne<Product>().WithMany().HasForeignKey(p => p.ProductId);

			modelBuilder.Entity<OrderOptionValues>().HasOne<OrderProducts>().WithMany().HasForeignKey(p => p.OrderProductId);
			modelBuilder.Entity<OrderOptionValues>().HasOne<ProductOption>().WithMany().HasForeignKey(p => p.ProductOptionId);
			modelBuilder.Entity<OrderOptionValues>().HasOne<ProductOptionValues>().WithMany().HasForeignKey(p => p.ProductOptionValueId);

		}
	}
}
