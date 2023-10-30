#nullable disable

using Microsoft.EntityFrameworkCore;
using Movie.DataAccess.Entities;

namespace Movie.DataAccess.Contexts
{
    //class where we will perform CRUD operations by accessing database tables via DbSets
    public class ShopAppContext : DbContext
	{
        //DbSet Properties for all entities
        public DbSet<SilverScreen> SilverScreens { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<SilverScreenCategory> SilverScreenCategories { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<User> Users { get; set; }

		public ShopAppContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //for many to many relationships
            modelBuilder.Entity<SilverScreenCategory>().HasKey(mc => new { mc.SilverScreenId, mc.CategoryId});
		}
    }
}

