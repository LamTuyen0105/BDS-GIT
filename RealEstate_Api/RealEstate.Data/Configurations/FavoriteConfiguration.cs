using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");
            builder.HasKey(x => new { x.UserId, x.PropertyId });
            builder.Property(x => x.Like).HasDefaultValue(1);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Favorites).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Property).WithMany(x => x.Favorites).HasForeignKey(x => x.PropertyId);
        }
    }
}
