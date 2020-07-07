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
            builder.Property(x => x.EvaluationStatusId).IsRequired();
            builder.Property(x => x.LegalPapersId).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ApartmentNumber).HasMaxLength(200).IsRequired();
            builder.Property(x => x.StreetNames).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.ContactName).HasMaxLength(100);
            builder.Property(x => x.EmailContact).HasMaxLength(100);
            builder.Property(x => x.ContactPhone).HasMaxLength(100);
            builder.Property(x => x.NumberOfStoreys).HasMaxLength(50);
            builder.Property(x => x.StartDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Status).HasDefaultValue(Status.UnApproved);
            builder.HasOne(x => x.TypeOfProperty).WithMany(x => x.Properties).HasForeignKey(x => x.TypeOfPropertyId);
            builder.HasOne(x => x.TypeOfTransaction).WithMany(x => x.Properties).HasForeignKey(x => x.TypeOfTransactionId);
            builder.HasOne(x => x.Ward).WithMany(x => x.Properties).HasForeignKey(x => x.WardId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Properties).HasForeignKey(x => x.UserID);
            builder.HasOne(x => x.Direction).WithMany(x => x.Properties).HasForeignKey(x => x.HouseDirectionId);
            builder.HasOne(x => x.EvaluationStatus).WithMany(x => x.Properties).HasForeignKey(x => x.EvaluationStatusId);
            builder.HasOne(x => x.LegalPaper).WithMany(x => x.Properties).HasForeignKey(x => x.LegalPapersId);
        }
    }
}
