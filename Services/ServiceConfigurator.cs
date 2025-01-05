using RentcastBackend.Interfaces;

namespace RentcastBackend.Services
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container
            services.AddControllers();
            services.AddHttpClient<IRentcastService, RentcastService>();

            // Swagger for API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Register your services here
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<Neo4jService>();

            // Register RentcastService with transient lifetime
            services.AddTransient<IRentcastService, RentcastService>();

            // Register HttpClient for RentcastService (if needed)
            services.AddHttpClient<RentcastService>();

            // Add other necessary services like controllers, etc.
            services.AddControllers();

        }
    }
}
