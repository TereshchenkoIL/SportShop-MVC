using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySportShop.Models.Models;


namespace MySportShop.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //        "Server=ASUS\\SQLEXPRESS;Database=SportShopdb;Trusted_Connection=True;MultipleActiveResultSets=true"

        //        );
        //    }
        //}

        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductInfo> ProductInfo{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>()
                .HasKey(c => new {c.ProductId,c.OrderId});            
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
