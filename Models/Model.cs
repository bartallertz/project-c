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
            modelBuilder.Entity<Product>()
            .HasOne(k => k.Category)
            .WithMany(k2 => k2.Products)
            .HasForeignKey(k => k.CategoryId);
            modelBuilder.Entity<Product>()
            .HasOne(k => k.SubCategory)
            .WithMany(k2 => k2.Products)
            .HasForeignKey(k => k.SubCategoryId);
            modelBuilder.Entity<SubCategory>()
            .HasOne(k => k.Category)
            .WithMany(k2 => k2.SubCategories)
            .HasForeignKey(k => k.CategoryId);
            modelBuilder.Entity<ShoppingCart>()
            .HasKey(k => new { k.ProductId, k.UserId });
            modelBuilder.Entity<History>()
            .HasKey(k => new { k.Id });
            modelBuilder.Entity<History>()
            .Property(i => i.Status)
            .HasDefaultValue("Pending");
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
        public DbSet<History> History { get; set; }


    }

    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Voornaam")]
        [RegularExpression(@"^[a-zA-Z]+((\s|\-)[a-zA-Z]+)?$", ErrorMessage = "AUB Alleen maar letters, geen cijfers of speciale characters.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Voornaam mag niet kleiner zijn dan 2 en niet groter dan 30 letters AUB.")]
        public string Name { get; set; }
        [Display(Name = "Achternaam")]
        [RegularExpression(@"^[a-zA-Z]+((\s|\-)[a-zA-Z]+)?$", ErrorMessage = "AUB Alleen maar letters, geen cijfers of speciale characters.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Achternaam mag niet kleiner zijn dan 2 en niet groter dan 30 letters AUB.")]
        public string LastName { get; set; }
        [Display(Name = "Geboorte Datum")]
        [RegularExpression(@"^[0-9]{1,2}\/[0-9]{1,2}\/[0-9]{4}$", ErrorMessage = "In het formaat MM/DD/YYYY AUB.")]
        public string Birthday { get; set; }
        [Display(Name = "Wachtwoord")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,40}$", ErrorMessage = "Wachtwoord moet op z'n minst 8 en maximaal 40 tekens hebben, moet op z'n minst 1 hoofdletter en 1 cijfer in.")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public List<Favourite> Product { get; set; }
        [Display(Name = "Geslacht")]
        [RegularExpression(@"^(?:man|Man|vrouw|Vrouw)$", ErrorMessage = "man, Man, vrouw, Vrouw Alleen AUB.")]
        public string Gender { get; set; }
        [Display(Name = "Straatnaam")]
        [RegularExpression(@"^([a-zA-Z]\s*){10,40}$", ErrorMessage = "Alleen letters minimaal 10 maximaal 40 letters AUB.")]
        public string Street_Name { get; set; }
        [Display(Name = "Email adres")]
        [EmailAddress(ErrorMessage = "Geen geldige email.")]
        public string email { get; set; }
        [Display(Name = "Huisnummer")]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "Minmaal 1 en maximaal 6 getallen AUB.")]
        public string House_Number { get; set; }
        [Display(Name = "Address toevoeging 1st verdieping A/B/C.")]
        [RegularExpression(@"^([a-zA-Z0-9]\s*){1,20}$", ErrorMessage = "Alleen letters en cijfers minimaal 1 en maximaal 20 tekens.")]
        public string Addition { get; set; }
        [Display(Name = "Postcode")]
        [RegularExpression(@"^[1-9][0-9]{3}[ ]?(([a-rt-zA-RT-Z][a-zA-Z])|([sS][bce-rt-xBCE-RT-X]))", ErrorMessage = "Dit is geen geldige postcode, voer een geldige postcode in AUB.")]
        public string Postalcode { get; set; }
        [Display(Name = "Stad")]
        [RegularExpression(@"^([a-zA-Z]\s*){2,30}$", ErrorMessage = "Alleen letters toegestaan AUB")]
        public string City { get; set; }
        [Display(Name = "Telefoon nummer")]
        [RegularExpression(@"^([0-9]{10})", ErrorMessage = "Dit is geen geldig normaal Nederlands telefoon nummer, voer een geldig telefoon nummer in.")]
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
        [Display(Name = "Product naam")]
        [RegularExpression(@"^([a-zA-Z0-9]\s*){2,300}$", ErrorMessage = "Alleen Letters en cijfers, minimaal 2 maximaal 300 tekens.")]
        public string Name { get; set; }
        [Display(Name = "Beschrijving")]
        [StringLength(3000, MinimumLength = 10 , ErrorMessage = "Op z'n minst 10 en maximaal 3000 tekens AUB.")]
        public string Description { get; set; }
        [Display(Name = "Prijs")]
        [RegularExpression(@"^[€]?[0-9]*(\.)?[0-9]?[0-9]?$", ErrorMessage = "Niet een geldig formaat voor geld.")]
        public float Price { get; set; }
        [Display(Name = "Eerste afbeelding voor een product")]
        [StringLength(256, ErrorMessage = "URL mag niet langer zijn dan 256 tekens.")]
        public string FirstImg { get; set; }
        public List<ImageURL> imageURLs { get; set; }
        public List<Favourite> Users { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        [Display(Name = "Voorraad")]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "Alleen nummers AUB, minimaal 1 en maximaal 99,999 nummers.")]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
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
        [Display(Name = "URL van een image van een product")]
        [StringLength(256, ErrorMessage = "URL mag niet langer zijn dan 256 tekens.")]
        public string url { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    public class SubCategory
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Subcategory naam")]
        [RegularExpression(@"^([a-zA-Z]\s*){2,100}$", ErrorMessage = "Alleen letters en minimaal en maximaal 2 en 100 tekens respectivelijk.")]
        public string SubCategory_Name { get; set; }
        public List<Product> Products { get; set; }
        public int CategoryId { get; set; }
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
    public class History
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}