using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");
            builder.HasKey(x => new { x.Id, x.PropertyId });
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.LinkName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Caption).HasMaxLength(200);
            builder.HasOne(x => x.Property).WithMany(x => x.PropertyImages).HasForeignKey(x => x.PropertyId);
        }
    }
}
