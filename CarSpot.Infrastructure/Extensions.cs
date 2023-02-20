
using CarSpot.Api.Services;
using CarSpot.Application.Abstractions;
using CarSpot.Infrastructure.Auth;
using CarSpot.Infrastructure.DAL;
using CarSpot.Infrastructure.Exceptions;
using CarSpot.Infrastructure.Logging;
using CarSpot.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<ExceptionMiddleware>();
            services.AddSecurity();
            services.AddHttpContextAccessor();
            services
                .AddSingleton<IClock,Clock>()
                .AddPostgres(configuration);


            services
                .AddCustomLogging();

            var infrastructureAssembly = typeof(AppOptions).Assembly;

            services.Scan(s => s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddAuth(configuration);


            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
