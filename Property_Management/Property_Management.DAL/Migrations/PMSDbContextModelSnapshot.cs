﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Property_Management.DAL.Context;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    [DbContext(typeof(PMSDbContext))]
    partial class PMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PropertyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PropertyId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.InspectionCheck", b =>
                {
                    b.Property<string>("InspectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<string>("InspectedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NoOfDevicesDamaged")
                        .HasColumnType("int");

                    b.Property<int>("NoOfUnits")
                        .HasColumnType("int");

                    b.Property<int>("UnitIdNumber")
                        .HasColumnType("int");

                    b.HasKey("InspectionId");

                    b.HasIndex("InspectedBy");

                    b.HasIndex("ManagerId");

                    b.HasIndex(new[] { "UnitIdNumber" }, "IX_UnitIdNumber")
                        .IsUnique();

                    b.ToTable("InspectionCheck");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Lease", b =>
                {
                    b.Property<string>("LeaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("Lease_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Lease_End_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Lease_Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Lease_Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("Monthly_Rent")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PropertyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Security_Deposit_Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tenant_Property_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tenant_Unit_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Upcoming_Tenant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaseId", "TenantId");

                    b.HasIndex(new[] { "PropertyId" }, "IX_PropertyId")
                        .IsUnique();

                    b.HasIndex(new[] { "TenantId" }, "IX_TenantId")
                        .IsUnique();

                    b.HasIndex(new[] { "Tenant_Property_Number" }, "IX_Tenant_Property_Number")
                        .IsUnique();

                    b.ToTable("Leases");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.LeasePayment", b =>
                {
                    b.Property<string>("LeasePaymentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("LateFees")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LeaseDetailsLeaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaseDetailsTenantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaidBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PaymentAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeasePaymentId");

                    b.HasIndex("PaidBy");

                    b.HasIndex("LeaseDetailsLeaseId", "LeaseDetailsTenantId");

                    b.HasIndex(new[] { "LeaseId" }, "IX_LeaseId")
                        .IsUnique();

                    b.HasIndex(new[] { "LeaseId" }, "IX_LeasePaymentId")
                        .IsUnique();

                    b.HasIndex(new[] { "LeaseId" }, "IX_PaidBy")
                        .IsUnique();

                    b.ToTable("LeasePayments");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.MaintenanceRequest", b =>
                {
                    b.Property<string>("RequestId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoggedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MRNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex(new[] { "LoggedBy" }, "IX_LoggedBy")
                        .IsUnique();

                    b.ToTable("MaintenanceRequests");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Manager", b =>
                {
                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerId");

                    b.HasIndex(new[] { "Email" }, "IX_Email")
                        .IsUnique();

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Property", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PropertyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("managerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propertyTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyId");

                    b.ToTable("Propertys");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Tenant", b =>
                {
                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Concurrency")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TenantId");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "IX_Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Email1");

                    b.HasIndex(new[] { "TenantId" }, "IX_TenantId")
                        .IsUnique()
                        .HasDatabaseName("IX_TenantId1");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Property_Management.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.InspectionCheck", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("InspectedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Property_Management.DAL.Entities.Manager", null)
                        .WithMany("InspectionChecks")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Lease", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.LeasePayment", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Tenant", "TenantDetails")
                        .WithMany()
                        .HasForeignKey("PaidBy");

                    b.HasOne("Property_Management.DAL.Entities.Lease", "LeaseDetails")
                        .WithMany()
                        .HasForeignKey("LeaseDetailsLeaseId", "LeaseDetailsTenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaseDetails");

                    b.Navigation("TenantDetails");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.MaintenanceRequest", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Tenant", "TenantDetails")
                        .WithMany()
                        .HasForeignKey("LoggedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantDetails");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Tenant", b =>
                {
                    b.HasOne("Property_Management.DAL.Entities.Property", "Property")
                        .WithOne("TenantDetail")
                        .HasForeignKey("Property_Management.DAL.Entities.Tenant", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Manager", b =>
                {
                    b.Navigation("InspectionChecks");
                });

            modelBuilder.Entity("Property_Management.DAL.Entities.Property", b =>
                {
                    b.Navigation("TenantDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
