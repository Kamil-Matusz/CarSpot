using CarSpot.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Configurations
{
    internal sealed class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
    {
        public void Configure(EntityTypeBuilder<CleaningReservation> builder)
        {  
        }
    }
}
