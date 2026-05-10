using Booking.Core.Models.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Data.Configurations
{

    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasMany(t => t.Tickets)
                .WithOne(tk => tk.Trip)
                .HasForeignKey(tk => tk.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.FromCity).IsRequired().HasMaxLength(100);
            builder.Property(t => t.ToCity).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)");
        }
    }
}
