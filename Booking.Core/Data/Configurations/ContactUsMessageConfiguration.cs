using Booking.Core.Models.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Data.Configurations
{
    public class ContactUsMessageConfiguration : IEntityTypeConfiguration<ContactUsMessage>
    {
        public void Configure(EntityTypeBuilder<ContactUsMessage> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Company).HasMaxLength(150);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(30);
            builder.Property(x => x.Address).HasMaxLength(250);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(u => u.ContactUsMessages)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
