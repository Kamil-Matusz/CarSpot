using CarSpot.Core.Repositories;
using CarSpot.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        // Registration DbContextService
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=CarSpot;Username=postgres;Password=";
            services.AddDbContext<CarSpotDbContext>(x => x.UseNpgsql(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npg.EnableLegacyTimestampBehavior", true);

            return services;
        }
    }
}
