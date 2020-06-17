using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using RealEstate.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Area).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Length).IsRequired();
            builder.Property(x => x.Width).IsRequired();
            builder.Property(x => x.TypeOfPropertyId).IsRequired();
            builder.Property(x => x.TypeOfTransactionId).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Image).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.LegalPapers).IsRequired(false);
            builder.Property(x => x.HouseDirection).IsRequired(false);
            builder.Property(x => x.StartDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Status).HasDefaultValue(Status.UnApproved);
            builder.HasOne(x => x.TypeOfProperty).WithMany(x => x.Properties).HasForeignKey(x => x.TypeOfPropertyId);
        }
    }
}
