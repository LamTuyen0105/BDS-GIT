using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class ImagePropertyConfiguration : IEntityTypeConfiguration<ImageProperty>
    {
        public void Configure(EntityTypeBuilder<ImageProperty> builder)
        {
            builder.ToTable("ImageProperties");
            builder.HasKey(x => new { x.Id, x.PropertyId });
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.LinkName).HasMaxLength(200).IsRequired();
            builder.HasOne(x => x.Property).WithMany(x => x.ImageProperties).HasForeignKey(x => x.PropertyId);
        }
    }
}
