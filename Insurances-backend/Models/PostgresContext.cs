using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Insurances_backend.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public virtual DbSet<NewInquiry> NewInquiries { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=34.165.45.205;Port=5432;Database=postgres;Username=postgres    ;Password=898");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FormCompletionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("form_completion_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<InsuranceCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("insurance_companies_pkey");

            entity.ToTable("insurance_companies");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<NewInquiry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("new_inquiries_pkey");

            entity.ToTable("new_inquiries");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.SalaryAmount).HasColumnName("salary_amount");
            entity.Property(e => e.StatusCode).HasColumnName("status_code");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.NewInquiries)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_new_inquiries_status");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("policy_pkey");

            entity.ToTable("policy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("client_id");
            entity.Property(e => e.CompanyCode).HasColumnName("company_code");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");

            entity.HasOne(d => d.Client).WithMany(p => p.Policies)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_policy_client");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
