using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Configurations;
using RealEstate.Data.Entity;
using RealEstate.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.EF
{
    public class RealEstateDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public RealEstateDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new DirectionConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationStatusConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
            modelBuilder.ApplyConfiguration(new LegalPaperConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WardConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);


            modelBuilder.Seed();
        }

        public DbSet<Direction> Directions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<EvaluationStatus> EvaluationStatuses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<LegalPaper> LegalPapers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<TypeOfProperty> TypeOfProperties { get; set; }
        public DbSet<TypeOfTransaction> TypeOfTransactions { get; set; }
        public DbSet<Ward> Wards { get; set; }
    }
}