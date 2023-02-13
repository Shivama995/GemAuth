using Application;
using Application.Authentication.CommandHandlers;
using Common.Configuration;
using Common.Configuration.Implementations;
using Common.Cryptography;
using Common.Cryptography.Implementations;
using Common.Logger;
using Common.Logger.Implementations;
using Common.Redis.Implementations;
using Data;
using MediatR;
using Serilog;
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
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<ICrypt, Crypt>();
            services.AddTransient<IRedis, Redis>();
            //services.AddLogging();
            Log.Logger = new LoggerConfiguration().CreateLogger();
            services.AddLogging(delegate (ILoggingBuilder loggingBuilder)
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(Log.Logger, true);
            });
            services.AddSingleton(Log.Logger);
            services.AddTransient<IGemLogger, GemLogger>();
            services.AddTransient<IConfigManager, ConfigManager>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
