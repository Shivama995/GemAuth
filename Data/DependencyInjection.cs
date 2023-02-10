using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services)
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
