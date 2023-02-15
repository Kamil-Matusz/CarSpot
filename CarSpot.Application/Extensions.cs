using CarSpot.Api.Services;
using CarSpot.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CarSpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IReservationsService, ReservationsService>();

            // registration ICommands
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
