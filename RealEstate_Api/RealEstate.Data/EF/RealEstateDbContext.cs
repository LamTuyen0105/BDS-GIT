using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Configurations;
using RealEstate.Data.Entity;
using RealEstate.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.EF
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new ImagePropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new StreetConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WardConfiguration());

            modelBuilder.Seed();
        }

        public DbSet<District> Districts { get; set; }
        public DbSet<ImageProperty> ImageProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<TypeOfProperty> TypeOfProperties { get; set; }
        public DbSet<TypeOfTransaction> TypeOfTransactions { get; set; }
        public DbSet<Ward> Wards { get; set; }
    }
}
