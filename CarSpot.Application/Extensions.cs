using CarSpot.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarSpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationsService, ReservationsService>();

            return services;
        }
    }
}
