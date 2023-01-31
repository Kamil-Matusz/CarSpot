using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL
{
    // auto migration if don't exist
    internal sealed class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CarSpotDbContext>();
                dbContext.Database.Migrate();

                var weeklyParkingSpot = dbContext.WeeklyParkingSpots.ToList();
                if (weeklyParkingSpot.Any())
                {
                    return Task.CompletedTask;
                }
                var clock = new Clock();
                weeklyParkingSpot = new List<WeeklyParkingSpot>()
                {
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P1"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P2"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P3"),
                };
                dbContext.AddRange(weeklyParkingSpot);
                dbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
