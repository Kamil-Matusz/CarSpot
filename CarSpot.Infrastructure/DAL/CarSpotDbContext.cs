using CarSpot.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL
{
    internal class CarSpotDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

        public CarSpotDbContext(DbContextOptions<CarSpotDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
                {
                    property.ValueGenerated = ValueGenerated.Never;
                }
            }
        }
    }
}
