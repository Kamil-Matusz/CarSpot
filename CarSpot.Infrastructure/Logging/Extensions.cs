using CarSpot.Application.Abstractions;
using CarSpot.Infrastructure.Logging.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.Logging
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomLogging(this IServiceCollection services)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            return services;
        }
    }
}
