using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.View).HasDefaultValue(0);
            builder.Property(x => x.Share).HasDefaultValue(0);
            builder.Property(x => x.DateSubmitted).HasDefaultValue(DateTime.Now).IsRequired();
        }
    }
}
