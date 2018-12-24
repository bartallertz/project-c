using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            modelBuilder.Entity<ImageURL>()
            .HasOne(k => k.Product)
            .WithMany(k2 => k2.imageURLs)
            .HasForeignKey(k => k.ProductId);
            modelBuilder.Entity<ShoppingCart>()
            .HasKey(k => new { k.ProductId, k.UserId });
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
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zA-Z]+((\s|\-)[a-zA-Z]+)?$", ErrorMessage = "Not a valid character inserted")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Can't be longer then 30 Characters and has to be atleast 2")]
        public string Name { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+((\s|\-)[a-zA-Z]+)?$", ErrorMessage = "Not a valid character inserted")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Can't be longer then 30 Characters and has to be atleast 2")]
        public string LastName { get; set; }
        [Display(Name = "Your age")]
        [Range(13,120, ErrorMessage = "Minimum age is 13 maximum age is 120")]
        [RegularExpression(@"^([1-9][0-9]{1,2}?|)$", ErrorMessage = "not a valid character")]
        [StringLength(3)]
        public string Age { get; set; }
        [Display(Name = "Password")]
        [StringLength(40)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,40}$", ErrorMessage = "Invalid Characters only Alphanumerical Characters allowed, has to have atleast 1 number and 1 Capital letter")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public List<Favourite> Product { get; set; }
        [Display(Name = "Gender")]
        [StringLength(6)]
        [RegularExpression(@"^(?:man|Man|male|Male|woman|Woman|female|Female)$", ErrorMessage = "Male, Man, Female, Woman only please")]
        public string Gender { get; set; }
        [Display(Name = "Street name")]
        [StringLength(40)]
        [RegularExpression(@"^([a-zA-Z]\s*){10,40}$", ErrorMessage = "Only letters allowed minimum of 10 maximum of 40 characters")]
        public string Street_Name { get; set; }
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Display(Name = "Number")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "has to be bigger then 1 and smaller then 999.999")]
        [RegularExpression(@"^[+]?\d*$", ErrorMessage = "Only numbers please")]
        public string House_Number { get; set; }
        [Display(Name = "Address addition (A/B/1st Floor etc)")]
        [StringLength(20)]
        [RegularExpression(@"^([a-zA-Z0-9]\s*){1,20}$", ErrorMessage = "Only Alphanumerical characters minimum 2 and max 30 Characters")]
        public string Addition { get; set; }
        [Display(Name = "Postalcode")]
        [StringLength(7)]
        [RegularExpression(@"^[1-9][0-9]{3}[ ]?(([a-rt-zA-RT-Z][a-zA-Z])|([sS][bce-rt-xBCE-RT-X]))", ErrorMessage = "Not a valid Dutch PostalCode")]
        public string Postalcode { get; set; }
        [Display(Name = "City")]
        [StringLength(30)]
        [RegularExpression(@"^([a-zA-Z]\s*){2,30}$", ErrorMessage = "No special characters nor numbers allowed")]
        public string City { get; set; }
        [Display(Name = "Phone number")]
        [StringLength(10)]
        [RegularExpression(@"^([0-9]{10})", ErrorMessage = "Not a valid normal Dutch Phone number")]
        public string Telephone_Number { get; set; }
        public Role Role { get; set; }
    
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
        [Display(Name = "Product name")]
        [StringLength(100)]
        [RegularExpression(@"^([a-zA-Z0-9]\s*){2,100}$", ErrorMessage = "Alphanumerical characters allowed, minimum 2 maximum of 100 characters")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [StringLength(3000, MinimumLength = 10 ,ErrorMessage = "Max length of the description is 3000 Characters")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        [RegularExpression(@"^[€]?[0-9]*(\.)?[0-9]?[0-9]?$", ErrorMessage = "Not a valid format for Currancy")]
        public float Price { get; set; }
        [Display(Name = "Url of the first Image of a product")]
        [StringLength(256, ErrorMessage = "URL cannot be longer then 256 characters")]
        public string FirstImg { get; set; }
        public List<ImageURL> imageURLs { get; set; }
        public List<Favourite> Users { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        [Display(Name = "Stock")]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "Not a valid character, Max size = 99999")]
        [Range(0,99999, ErrorMessage = "Only numbers please, can't go past 99999")]
        public int Stock { get; set; }
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
        [Display(Name = "Url of a product image")]
        [StringLength(256, ErrorMessage = "URL cannot be longer then 256 characters")]
        public string url { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    public class SubCategory
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Subcategory name")]
        [StringLength(100)]
        [RegularExpression(@"^([a-zA-Z]\s*){2,100}$", ErrorMessage = "Only Letters allowed min and max length of 3,100 respectively")]
        public string SubCategory_Name { get; set; }
    }
    public class ShoppingCart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Amount")]
        [Range(0,100)]
        public int Amount { get; set; }

    }

}