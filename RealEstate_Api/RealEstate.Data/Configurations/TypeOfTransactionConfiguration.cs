using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class TypeOfTransactionConfiguration : IEntityTypeConfiguration<TypeOfTransaction>
    {
        public void Configure(EntityTypeBuilder<TypeOfTransaction> builder)
        {
            builder.ToTable("TypeOfTransactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.TypeOfTransactionName).HasMaxLength(100).IsRequired();
        }
    }
}
