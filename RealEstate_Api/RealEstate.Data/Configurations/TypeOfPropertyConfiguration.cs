using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class TypeOfPropertyConfiguration : IEntityTypeConfiguration<TypeOfProperty>
    {
        public void Configure(EntityTypeBuilder<TypeOfProperty> builder)
        {
            builder.ToTable("TypeOfPrperties");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.TypeOfPropertyName).HasMaxLength(50).IsRequired();
        }
    }
}
