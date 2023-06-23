using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public class AbilityContext : IdentityDbContext
{
    public DbSet<User> AppUsers => Set<User>();

    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();



    public AbilityContext(DbContextOptions<AbilityContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<Product>()
            .HasMany(p => p.Users)
             .WithMany(u => u.Products)
              .UsingEntity("Wishlist");


        //builder.Entity<User>()
        //    .HasMany(p => p.Orders)
        //    .WithOne(c => c.User)
        //    .HasForeignKey(p => p.UserId)
        //    .IsRequired(false) //==> Redundant
        //    .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<IdentityUser>()
            .UseTptMappingStrategy();

    }
}
