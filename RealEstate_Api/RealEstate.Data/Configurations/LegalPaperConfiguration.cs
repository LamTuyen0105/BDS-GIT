using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class LegalPaperConfiguration : IEntityTypeConfiguration<LegalPaper>
    {
        public void Configure(EntityTypeBuilder<LegalPaper> builder)
        {
            builder.ToTable("LegalPapers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TypeOfLegalPapers).HasMaxLength(50).IsRequired();
        }
    }
}
