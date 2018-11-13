using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectC.model
{
    public class ProjectContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favourite>()
            .HasKey(k => new { k.ProductId, k.UserId });
            modelBuilder.Entity<Favourite>()
            .HasOne(k => k.User)
            .WithMany(k2 => k2.Product)
            .HasForeignKey(k => k.UserId);
            modelBuilder.Entity<Favourite>()
            .HasOne(k => k.Product)
            .WithMany(k2 => k2.Users)
            .HasForeignKey(k => k.ProductId);
        }
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ImageURL> imageURLs { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Favorite> favourites { get; set; }



    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Favorite> Product { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }

    public class Favorite
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string FirstImg { get; set; }
        public List<ImageURL> imageURLs { get; set; }
        public List<Favorite> Users { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }

    public class ImageURL
    {
        public string url { get; set; }
        public int Id { get; set; }
        public Product product { get; set; }
    }
    public class SubCategory
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string SubCategory_Name { get; set; }
    }

}