using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerceAPI.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<GoogleAuth> GoogleAuths { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Electronics" },
                new Category { Id = 2, CategoryName = "Clothing" },
                new Category { Id = 3, CategoryName = "Books" },
                new Category { Id = 4, CategoryName = "Home & Kitchen" },
                new Category { Id = 5, CategoryName = "Toys" },
                new Category { Id = 6, CategoryName = "Sports" },
                new Category { Id = 7, CategoryName = "Beauty" }
            );

            modelBuilder.Entity<SubCategory>().HasData(
        new SubCategory { Id = 1, SubCategoryName = "Laptops", CategoryId = 1 },
        new SubCategory { Id = 2, SubCategoryName = "Phones", CategoryId = 1 },
        new SubCategory { Id = 3, SubCategoryName = "Cameras", CategoryId = 1 },

        new SubCategory { Id = 4, SubCategoryName = "Men", CategoryId = 2 },
        new SubCategory { Id = 5, SubCategoryName = "Women", CategoryId = 2 },
        new SubCategory { Id = 6, SubCategoryName = "Accessories", CategoryId = 2 },

        new SubCategory { Id = 7, SubCategoryName = "Fiction", CategoryId = 3 },
        new SubCategory { Id = 8, SubCategoryName = "Non-Fiction", CategoryId = 3 },
        new SubCategory { Id = 9, SubCategoryName = "Comics", CategoryId = 3 },

        new SubCategory { Id = 10, SubCategoryName = "Furniture", CategoryId = 4 },
        new SubCategory { Id = 11, SubCategoryName = "Kitchen", CategoryId = 4 },
        new SubCategory { Id = 12, SubCategoryName = "Decor", CategoryId = 4 },

        new SubCategory { Id = 13, SubCategoryName = "Action Figures", CategoryId = 5 },
        new SubCategory { Id = 14, SubCategoryName = "Puzzles", CategoryId = 5 },
        new SubCategory { Id = 15, SubCategoryName = "Board Games", CategoryId = 5 },

        new SubCategory { Id = 16, SubCategoryName = "Fitness", CategoryId = 6 },
        new SubCategory { Id = 17, SubCategoryName = "Outdoor", CategoryId = 6 },
        new SubCategory { Id = 18, SubCategoryName = "Team Sports", CategoryId = 6 },

        new SubCategory { Id = 19, SubCategoryName = "Skincare", CategoryId = 7 },
        new SubCategory { Id = 20, SubCategoryName = "Makeup", CategoryId = 7 },
        new SubCategory { Id = 21, SubCategoryName = "Haircare", CategoryId = 7 }
    );
        

        modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<BasketProduct>()
    .HasKey(bp => new { bp.BasketId, bp.ProductId });

            modelBuilder.Entity<BasketProduct>()
                .HasOne(bp => bp.Basket)
                .WithMany(b => b.BasketProducts)
                .HasForeignKey(bp => bp.BasketId);

            modelBuilder.Entity<BasketProduct>()
                .HasOne(bp => bp.Product)
                .WithMany(p => p.BasketProducts)
                .HasForeignKey(bp => bp.ProductId);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany(u => u.Baskets);

            modelBuilder.Entity<Auth>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<GoogleAuth>()
                .HasOne(g => g.User)
                .WithMany(u => u.GoogleAuths) 
                .HasForeignKey(g => g.UserId);


            modelBuilder.Entity<Token>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);


        }
    }
}
