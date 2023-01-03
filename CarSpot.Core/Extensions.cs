using Microsoft.Extensions.DependencyInjection;

namespace CarSpot.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }
    }
}
