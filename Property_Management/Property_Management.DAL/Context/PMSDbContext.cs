using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Entities;

namespace Property_Management.DAL.Context
{
    public class PMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LandLord> LordLords { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<WorkOrderVendor> WorkOrderVendors { get; set; }
        public DbSet<SecurityDepositReturn> SecurityDepositReturns { get; set; }
        public DbSet<InspectionCheck> InspectionChecks { get; set; }
        public DbSet<Staff> Employees { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
                entity.Property(u => u.LandLordId).IsRequired(false);
                entity.Property(u => u.PropertyId).IsRequired(false);
                entity.Property(u => u.UnitId).IsRequired(false);
                entity.Property(u => u.NormalizedMoveInDate).IsRequired(false);
                entity.Property(u => u.NormalizedMoveOutDate).IsRequired(false);
                entity.Property(u => u.Address).IsRequired(false);
                entity.HasIndex(u => u.Email, "IX_UniqueEmail").IsUnique();
                entity.HasIndex(u => u.PhoneNumber, "IX_UniquePhoneNumber").IsUnique();
                entity.HasOne(p => p.Units).WithMany(p => p.Tenants).HasForeignKey(p => p.UnitId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LandLord>(entity =>
            {
                entity.Property(prop => prop.TenantId).IsRequired(false);
                entity.Property(prop => prop.PropertyId).IsRequired(false);
                entity.Property(prop => prop.Occupation).IsRequired(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(prop => prop.TransactionRefereal).IsRequired(false);
            });

            modelBuilder.Entity<Unit>()
                .Property(t => t.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<InspectionCheck>()
                .HasOne(p => p.Employees)
                .WithMany(p => p.InspectionChecks)
                .HasForeignKey(p => p.InspectedBy)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<WorkOrder>()
           .HasOne(p => p.MaintenanceRequest)
           .WithMany(p => p.WorkOrder)
           .HasForeignKey(p => p.MaintenanceRequestId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasOne(p => p.Tenants)
  .WithMany(p => p.MaintenanceRequests)
  .HasForeignKey(p => p.LoggedBy)
  .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<MaintenanceRequest>()
       .HasOne(p => p.Employees)
       .WithMany(p => p.MaintenanceRequests)
       .HasForeignKey(p => p.ReportedTo)
       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(prop => prop.LeaseId).IsRequired(false);
                entity.HasOne(p => p.LandLord)
                .WithMany(p => p.Property)
                .HasForeignKey(p => p.LandLordId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(p => p.Lease)
             .WithMany(p => p.Property)
             .HasForeignKey(p => p.LeaseId)
             .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SecurityDepositReturn>()
            .HasOne(p => p.Units)
            .WithMany(p => p.SecurityDepositReturns)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.Property(prop => prop.UpcomingTenant).IsRequired(false);
                entity.Property(prop => prop.PaymentId).IsRequired(false);
                entity.Property(prop => prop.Description).IsRequired(true);
                entity.HasOne(p => p.Tenant).WithMany(p => p.Lease).HasForeignKey(p => p.TenantId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(p => p.SecurityDepositReturns).WithMany().HasForeignKey(p => p.UpcomingTenant).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(prop => prop.LeaseId).IsRequired(true);
            });

            modelBuilder.Entity<SecurityDepositReturn>()
               .HasOne(p => p.Lease)
               .WithMany()
               .HasForeignKey(p => p.LeavingTenant)
               .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
    }
}