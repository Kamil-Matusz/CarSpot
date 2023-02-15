using CarSpot.Core.Entities;
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
    internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
    {
        public void Configure(EntityTypeBuilder<VehicleReservation> builder)
        {
            builder.Property(x => x.BookerName)
                .HasConversion(x => x.Value, x => new BookerName(x));
            builder.Property(x => x.Capacity)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Capacity(x));
        }
    }
}
