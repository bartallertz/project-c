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
            modelBuilder.Entity<User>()
            .Property(i => i.RoleId)
            .HasDefaultValue(1);

            modelBuilder.Entity<ShoppingCart>()
            .HasKey(k => new{k.ProductId, k.UserId});
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
        public DbSet<Favourite> favourites { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }


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
        public List<Favourite> Product { get; set; }
        public string Gender { get; set; }
        public string Street_Name { get; set; }
        public string email { get; set; }
        public int House_Number { get; set; }
        public string Addition { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Telephone_Number { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }

    public class Favourite
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
        public List<Favourite> Users { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
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
    public class ShoppingCart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

    }

}