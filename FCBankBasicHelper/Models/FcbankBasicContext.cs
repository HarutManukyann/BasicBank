using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FCBankBasicHelper.Models;

public partial class FcbankBasicContext : DbContext
{
    public FcbankBasicContext()
    {
    }

    public FcbankBasicContext(DbContextOptions<FcbankBasicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRolesMapping> UserRolesMappings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=FC-PROG-43\\MSSQLSERVER02;Database=FCBankBasic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK__Config__C41E0288F3135C58");

            entity.ToTable("Config");

            entity.Property(e => e.Key).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC074E21C13D");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Passport, "UQ__tmp_ms_x__208C1D4D76E6F96A").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__tmp_ms_x__5C7E359E106CAD83").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__A9D10534A3146E82").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Passport)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Version)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Phone__3214EC07E8BF6313");

            entity.ToTable("Phone");

            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Phones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Phone__UserId__3F115E1A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07DE532320");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.ToTable("Token");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RefreshToken).HasMaxLength(50);
            entity.Property(e => e.RefreshTokenExpire).HasColumnType("date");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC072DCCD4BD");

            entity.HasIndex(e => e.PassportNumber, "UQ__tmp_ms_x__45809E7121A80BA9").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__tmp_ms_x__536C85E4E9875AAB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__A9D1053431BED6C2").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(70);
            entity.Property(e => e.FirstName).HasMaxLength(70);
            entity.Property(e => e.LastName).HasMaxLength(70);
            entity.Property(e => e.PassportNumber).HasMaxLength(70);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.Username).HasMaxLength(70);
        });

        modelBuilder.Entity<UserRolesMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC079BBE8A09");

            entity.ToTable("UserRolesMapping");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRolesMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__RoleI__51300E55");

            entity.HasOne(d => d.User).WithMany(p => p.UserRolesMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__UserI__503BEA1C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
