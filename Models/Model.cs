using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace projectC.model
{
    public class ProjectContext : DbContext
    {

         public ProjectContext(DbContextOptions<ProjectContext> options): base(options)
        {   
        }
        
    public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products {get; set;}
        
        // public DbSet<Rolls> Rolls {get; set;}
        // public DbSet<User> Users {get; set;}
        // public DbSet<Assortiment> Assortiments {get; set;}
        // public DbSet<ShoppingCart> ShoppingCarts{get; set;}
        // public DbSet<Favorite> Favorites {get; set;}

        // public DbSet<ProductWagen> ProductWagens{get; set;}
        


    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<Product> Product{get; set;}
        public Category category{get; set;}

    }
    public class Product
    {
        public int Id{get; set;}
        public string Name {get; set;}
        public string Beschrijving {get; set;}
        public decimal Prijs{get;set;}
        public Category categories {get; set; }

        // public Assortiment assortiment{get; set;}

        // public ProductWagen productWagen{get; set;}

        // public Favorite Favorite{get; set;}

        
    }
    // public class Rolls
    // {
    //     public int Id {get; set;}
    //     public string Name{get; set;}

    //     public User user{get; set;}

    // }
    // public class User
    // {
    //     public int Id{get; set;}
    //     public string UserName{get; set;}
    //     public int Age{get; set;}
    //     public string Password{get; set;}
    //     public string description {get; set;}
        
    //     public List<Rolls> Roll {get; set;}

    //     public Assortiment assortiment{get; set;}
    //     public Favorite favorite{get; set;}

    //     public ProductWagen productWagen{get; set;}
    // }
    // public class Assortiment
    // {
    //     public int Id{get; set; }
    //     public int Amount{get; set; }

    //     public List<User> Users{get; set;}
    //     public List<Product> Products{get; set;}
    // } 
    // public class ShoppingCart
    // {
    //     public int Id {get; set;}

    //     public ProductWagen productWagen{get; set;}
    // }
    // public class Favorite
    // {
    //     public int Id{get; set;}
        
    //     public List<Product> Product{get; set;}
    //     public List<User> Users{get; set;}
    // }
    // public class ProductWagen
    // {
    //     public int ProductWagenId {get; set; }
    //     public List<Product> Product{get; set;}

    //     public List<ShoppingCart> shoppingCart{get; set;}

    //     public List<User> user{get; set;}
    //}
    }