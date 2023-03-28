using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Entities;

namespace Property_Management.DAL.Context
{
    public class PMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Lease>? Leases { get; set; }
        public DbSet<Property>? Propertys { get; set; }
        public DbSet<LeasePayment>? LeasePayments { get; set; }
        public DbSet<MaintenanceRequest>? MaintenanceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(prop => prop.Concurrency).IsRequired(false);
                entity.Property(prop => prop.Occupation).IsRequired(false);
            });
            

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(prop => prop.Concurrency).IsRequired(false);
            });

            modelBuilder.Entity<LeasePayment>(entity =>
            {
                entity.HasIndex(prop => prop.LeaseId, $"IX_{nameof(LeasePayment.LeaseId)}").IsUnique(true);
                entity.HasIndex(prop => prop.LeasePaymentId, $"IX_{nameof(LeasePayment.LeasePaymentId)}").IsUnique(true);
                entity.HasIndex(prop => prop.UserId, $"IX_{nameof(LeasePayment.UserId)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasIndex(prop => prop.LandLordOrManagerId, $"IX_{nameof(Lease.LandLordOrManagerId)}").IsUnique(true);
                entity.HasIndex(prop => prop.TenantId, $"IX_{nameof(Lease.TenantId)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasIndex(prop => prop.UserId, $"IX_{nameof(MaintenanceRequest.UserId)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
            });

            modelBuilder.Entity<InspectionCheck>(entity =>
            {
                entity.HasIndex(prop => prop.UnitIdNumber, $"IX_{nameof(InspectionCheck.UnitIdNumber)}").IsUnique(true);
                entity.Property(prop => prop.Concurrency).IsRequired(false);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

