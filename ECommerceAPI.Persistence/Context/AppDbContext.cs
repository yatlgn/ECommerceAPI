using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerceAPI.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));

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

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany(u => u.Baskets);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Baskets)
                .WithMany(b => b.Products)
                .UsingEntity(j => j.ToTable("BasketProducts"));

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
