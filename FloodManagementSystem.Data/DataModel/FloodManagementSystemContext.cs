using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FloodManagementSystem.Data.DataModel
{
    public partial class FloodManagementSystemContext : DbContext
    {
        public FloodManagementSystemContext()
        {
        }

        public FloodManagementSystemContext(DbContextOptions<FloodManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityAudit> CityAudit { get; set; }
        public virtual DbSet<CityRequests> CityRequests { get; set; }
        public virtual DbSet<Disaster> Disaster { get; set; }
        public virtual DbSet<DisasterDetails> DisasterDetails { get; set; }
        public virtual DbSet<DistributerRequests> DistributerRequests { get; set; }
        public virtual DbSet<EffectedCities> EffectedCities { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<ResourceAudit> ResourceAudit { get; set; }
        public virtual DbSet<ResourceCollection> ResourceCollection { get; set; }
        public virtual DbSet<ResourceRequest> ResourceRequest { get; set; }
        public virtual DbSet<ResourceStatus> ResourceStatus { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<StateAudit> StateAudit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=FloodManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.RegionId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.RegionId);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.StateId);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateId);
            });

            modelBuilder.Entity<CityAudit>(entity =>
            {
                entity.Property(e => e.ResourceId).HasColumnName("resourceId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityAudit)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityAudit_City");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.CityAudit)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityAudit_Resources");
            });

            modelBuilder.Entity<CityRequests>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityRequests)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRequests_City");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.CityRequests)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityRequests_Resources");
            });

            modelBuilder.Entity<Disaster>(entity =>
            {
                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DisasterDetails>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.DisasterDetails)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisasterDetails_Disaster");
            });

            modelBuilder.Entity<DistributerRequests>(entity =>
            {
                entity.HasOne(d => d.Region)
                    .WithMany(p => p.DistributerRequests)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistributerRequests_Regions");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.DistributerRequests)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistributerRequests_Resources");
            });

            modelBuilder.Entity<EffectedCities>(entity =>
            {
                entity.HasOne(d => d.DisasterDetails)
                    .WithMany(p => p.EffectedCities)
                    .HasForeignKey(d => d.DisasterDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EffectedCities_Disaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.EffectedCities)
                    .HasForeignKey(d => d.Stateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EffectedCities_State");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<ResourceAudit>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.ResourceAudit)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceAudit_City");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.ResourceAudit)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceAudit_Regions");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ResourceAudit)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceAudit_State");
            });

            modelBuilder.Entity<ResourceCollection>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceCollection)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceCollection_Resources");

                entity.HasOne(d => d.Rgion)
                    .WithMany(p => p.ResourceCollection)
                    .HasForeignKey(d => d.RgionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceCollection_Regions");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.ResourceCollection)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceCollection_ResourceStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ResourceCollection)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ResourceCollection_AspNetUsers");
            });

            modelBuilder.Entity<ResourceRequest>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RequestDetails)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.DisasterDetails)
                    .WithMany(p => p.ResourceRequest)
                    .HasForeignKey(d => d.DisasterDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceRequest_DisasterDetails");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ResourceRequest)
                    .HasForeignKey<ResourceRequest>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceRequest_ResourceStatus");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.ResourceRequest)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceRequest_Regions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ResourceRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ResourceRequest_AspNetUsers");
            });

            modelBuilder.Entity<ResourceStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resources>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StateAudit>(entity =>
            {
                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.StateAudit)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateAudit_Resources");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.StateAudit)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateAudit_State");
            });
        }
    }
}
