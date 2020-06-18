using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class EvaluationStatusConfiguration : IEntityTypeConfiguration<EvaluationStatus>
    {
        public void Configure(EntityTypeBuilder<EvaluationStatus> builder)
        {
            builder.ToTable("EvaluationStatuses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EvaluationStatusName).HasMaxLength(50).IsRequired();
        }
    }
}
