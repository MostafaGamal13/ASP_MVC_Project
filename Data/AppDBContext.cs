using ASP.NET_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ASP.NET_MVC.Data
{
    public class AppDBContext : DbContext
    {
            public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
            {
            }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        public DbSet<Item> Items { get; set; }
            public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                  new Category() { Id = 1, Name = "Select Category" },
                  new Category() { Id = 2, Name = "Computers" },
                  new Category() { Id = 3, Name = "Mobiles" },
                  new Category() { Id = 4, Name = "Electric machines" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
