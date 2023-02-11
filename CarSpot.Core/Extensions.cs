using CarSpot.Core.DomainServices;
using CarSpot.Core.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace CarSpot.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IReservationPolicy, EmployeeReservationPolicy>();
            services.AddSingleton<IReservationPolicy, ManagerReservationPolicy>();
            services.AddSingleton<IReservationPolicy, BossReservationPolicy>();
            services.AddSingleton<IParkingReservationService, ParkingReservationService>();
            return services;
        }
    }
}
