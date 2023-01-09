using CarSpot.Api.Entities;
using CarSpot.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Configurations
{
    internal class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
    {
       
        public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
        {
            builder.HasKey(x => x.WeeklyParkingSpotId);
            builder.Property(x => x.WeeklyParkingSpotId)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));
        }
    }
}
