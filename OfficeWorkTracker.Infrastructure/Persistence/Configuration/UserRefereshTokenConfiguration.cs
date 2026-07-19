using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Infrastructure.Persistence.Configuration
{
    public class UserRefereshTokenConfiguration : IEntityTypeConfiguration<UserRefereshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefereshToken> builder)
        {
            builder.ToTable("UserRefereshTokens");
            builder.HasKey(x => x.id);
            builder.Property(x => x.RefereshToken)
                .IsRequired().HasMaxLength(500);

            builder.Property(x => x.DeviceName).HasMaxLength(500);
            builder.Property(x => x.IpAddress)
                  .HasMaxLength(50);

            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.Property(x => x.ExpiryDate)
                   .IsRequired();

            builder.Property(x => x.IsRevoked)
                   .IsRequired();

            //Relationships
            builder.HasOne(x => x.user)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.RefereshToken).IsUnique();

            builder.HasIndex(x => x.UserID).IsUnique(false);
        }
    }
}
