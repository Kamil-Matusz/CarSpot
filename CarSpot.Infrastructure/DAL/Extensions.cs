using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using CarSpot.Core.Repositories;
using CarSpot.Infrastructure.DAL.Decorators;
using CarSpot.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CarSpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        private const string SectionName = "postgres";
        // Registration DbContextService
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            services.Configure<DatabaseOptions>(section);
            /* var options = new DatabaseOptions();
             section.Bind(options);*/
            var options = configuration.GetOptions<DatabaseOptions>(SectionName);
            

            services.AddDbContext<CarSpotDbContext>(x => x.UseNpgsql(options.connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            // decorator pattern
            services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            // user repository
            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddHostedService<DatabaseInitializer>();
            AppContext.SetSwitch("Npg.EnableLegacyTimestampBehavior", true);

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
