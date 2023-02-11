using Application;
using Common.Configuration;
using Common.Configuration.Implementations;
using Common.Cryptography;
using Common.Cryptography.Implementations;
using Common.Redis.Implementations;
using Data;
using MediatR;
using System.Reflection;

namespace API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(WebApplicationBuilder builder)
        {
            Configuration = builder.Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen();


            InjectServices(services);
            services.AddApplication()
                .AddData();
            services.AddHttpContextAccessor();
        }
        private void InjectServices(IServiceCollection services)
        {
            //Injecting App Settings Configuration
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<ICrypt, Crypt>();
            services.AddTransient<IRedis, Redis>();
            services.AddTransient<IConfigManager, ConfigManager>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
