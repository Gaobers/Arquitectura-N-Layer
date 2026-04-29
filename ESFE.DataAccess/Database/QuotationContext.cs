using System;
using System.Collections.Generic;
using ESFE.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESFE.DataAccess;

public partial class QuotationContext : DbContext
{
    public QuotationContext()
    {
    }

    public QuotationContext(DbContextOptions<QuotationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<QuotationDetail> QuotationDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=sistema_cotizaciones;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E27CF2250F3");

            entity.ToTable("brands");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5254AC70C");

            entity.ToTable("products");

            entity.HasIndex(e => e.ProductCode, "UQ__products__AE1A8CC405DE04E0").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.PriceUnitPurchase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_unit_purchase");
            entity.Property(e => e.PriceUnitSale)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_unit_sale");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_code");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductStatus)
                .HasDefaultValue(true)
                .HasColumnName("product_status");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("supplier_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_brands");
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuotationId).HasName("PK__quotatio__7841D7DB8ED6C796");

            entity.ToTable("quotations");

            entity.HasIndex(e => e.QuotationNumber, "UQ__quotatio__5C85B685E889D5D1").IsUnique();

            entity.Property(e => e.QuotationId).HasColumnName("quotation_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("client_phone");
            entity.Property(e => e.PaymentMethodName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("payment_method_name");
            entity.Property(e => e.QuotationNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("quotation_number");
            entity.Property(e => e.QuotationRegistration)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("quotation_registration");
            entity.Property(e => e.QuotationStatus)
                .HasDefaultValue(true)
                .HasColumnName("quotation_status");
            entity.Property(e => e.SellerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("seller_name");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ValidityDays).HasColumnName("validity_days");

            entity.HasOne(d => d.User).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quotations_users");
        });

        modelBuilder.Entity<QuotationDetail>(entity =>
        {
            entity.HasKey(e => e.QuotationDetailId).HasName("PK__quotatio__131ABC6077562D60");

            entity.ToTable("quotation_details");

            entity.Property(e => e.QuotationDetailId).HasColumnName("quotation_detail_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.QuotationId).HasColumnName("quotation_id");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.Product).WithMany(p => p.QuotationDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quotation_details_products");

            entity.HasOne(d => d.Quotation).WithMany(p => p.QuotationDetails)
                .HasForeignKey(d => d.QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quotation_details_quotations");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__roles__CF32E443DA542098");

            entity.ToTable("roles");

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RolName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol_name");
            entity.Property(e => e.RolStatus)
                .HasDefaultValue(true)
                .HasColumnName("rol_status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FEC54FD2A");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserNickname, "UQ__users__E1E179E211F70FBA").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_name");
            entity.Property(e => e.UserNickname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_nickname");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_password");
            entity.Property(e => e.UserStatus)
                .HasDefaultValue(true)
                .HasColumnName("user_status");

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
