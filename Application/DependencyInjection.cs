using Application.Authentication.Services;
using Application.Authentication.Services.Implementations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplicationServices();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithTransientLifetime());
            return services;
        }
    }
}
