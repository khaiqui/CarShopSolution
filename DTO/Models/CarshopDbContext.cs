using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DTO.Models;

public partial class CarshopDbContext : DbContext
{
    public CarshopDbContext()
    {
    }

    public CarshopDbContext(DbContextOptions<CarshopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserService> UserServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionStringDB"];
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__D2130A66E1D2F8D8");

            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.DiscountRate).HasColumnName("discountRate");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__0215CC59E3A531B9");

            entity.Property(e => e.ModelId).HasColumnName("modelId");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modelName");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderDet__BAD83E6B178CC607");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__59FA5E80");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__produ__5AEE82B9");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__2D10D16A6010B630");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.ModelId).HasColumnName("modelId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productName");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__Products__discou__534D60F1");

            entity.HasOne(d => d.Model).WithMany(p => p.Products)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK__Products__modelI__52593CB8");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__ProductO__0809335DA0C56E53");

            entity.ToTable("ProductOrder");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("date");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProductOr__userI__571DF1D5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF4025160B");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5725D766B5F").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("Customer")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__UserServ__455070DFF9AFCFDA");

            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("date");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserServi__userI__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
