using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Entities;

namespace Property_Management.DAL.Context
{
    public class PMSDbContext : DbContext
    {
            public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

            public DbSet<LeaseDetail> LeaseDetails { get; set; }
            public DbSet<Property> Propertys { get; set; }
            public DbSet<TenantDetail> TenantDetails { get; set; }
            public DbSet<Manager> Managers { get; set; }
            public DbSet<LeasePayment> leasePayments { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TenantDetail>(entity =>
                {
                    entity.Property(prop => prop.Address).IsRequired(false);
                    entity.HasIndex(prop => prop.Email, $"IX_{nameof(TenantDetail.Email)}").IsUnique(true);
                    entity.HasIndex(prop => prop.TenantId, $"IX_{nameof(TenantDetail.TenantId)}").IsUnique(true);
                    entity.Property(prop => prop.Concurrency).IsRequired(false);


                });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(prop => prop.Address).IsRequired(false);
                entity.HasIndex(prop => prop.Email, $"IX_{nameof(TenantDetail.Email)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);

            });

            modelBuilder.Entity<Property>(entity =>
            {
                
                entity.Property(prop => prop.Concurrency).IsRequired(false);
                entity.HasOne(prop => prop.TenantDetail)
                .WithOne(t => t.Property)
                .HasForeignKey<TenantDetail>(t => t.PropertyId);
            });

            modelBuilder.Entity<LeasePayment>(entity =>
            {
                entity.HasIndex(prop => prop.LeaseId, $"IX_{nameof(LeasePayment.LeaseId)}").IsUnique(true);
                entity.HasIndex(prop => prop.LeaseId, $"IX_{nameof(LeasePayment.LeasePaymentId)}").IsUnique(true);
                entity.HasIndex(prop => prop.LeaseId, $"IX_{nameof(LeasePayment.PaidBy)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);

                entity.HasOne(prop => prop.TenantDetails)
               .WithMany()
               .HasForeignKey(prop => prop.PaidBy);
            });

            modelBuilder.Entity<LeaseDetail>(entity =>
            {
                entity.HasIndex(prop => prop.TenantId, $"IX_{nameof(LeaseDetail.TenantId)}").IsUnique(true);
                entity.HasIndex(prop => prop.PropertyId, $"IX_{nameof(LeaseDetail.PropertyId)}").IsUnique(true);
                entity.HasIndex(prop => prop.Tenant_Property_Number, $"IX_{nameof(LeaseDetail.Tenant_Property_Number)}").IsUnique(true);

                entity.HasKey(prop => new { prop.LeaseId, prop.TenantId });
                entity.Property(prop => prop.Concurrency).IsRequired(false);


            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasIndex(prop => prop.LoggedBy, $"IX_{nameof(MaintenanceRequest.LoggedBy)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
                entity.HasOne(prop => prop.TenantDetails)
               .WithMany()
               .HasForeignKey(prop => prop.LoggedBy);
            });

            modelBuilder.Entity<InspectionCheck>(entity =>
            {
                entity.HasIndex(prop => prop.UnitIdNumber, $"IX_{nameof(InspectionCheck.UnitIdNumber)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
                entity.HasOne(prop => prop.Manager)
               .WithMany()
               .HasForeignKey(prop => prop.InspectedBy);
            });
            base.OnModelCreating(modelBuilder);
            }
        }
    }

