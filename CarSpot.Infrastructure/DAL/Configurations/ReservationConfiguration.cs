using CarSpot.Api.Entities;
using CarSpot.Core.Entities;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Configurations
{
    internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.ReservationId);
            builder.Property(x => x.ReservationId)
            .HasConversion(x => x.Value, x => new ReservationId(x));
            builder.Property(x => x.ParkingSpotId)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));
            builder.Property(x => x.Capacity)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Capacity(x));
            builder.Property(x => x.ReservationDate)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Date(x));

            //distinguishing the type of reservation using types in one table
            builder
                .HasDiscriminator<string>("Type")
                .HasValue<CleaningReservation>(nameof(CleaningReservation))
                .HasValue<VehicleReservation>(nameof(VehicleReservation));
        }
    }
}
