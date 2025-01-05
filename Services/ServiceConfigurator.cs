using RentcastBackend.Interfaces;
using Neo4jClient;
using Microsoft.Extensions.Options;
using RentcastBackend.Configurations;

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

            //Register IGraphClient
            services.AddSingleton<IGraphClient>(provider =>
            {
                var neo4jSettings = provider.GetRequiredService<IOptions<Neo4jSettings>>().Value;

                var client = new BoltGraphClient(
                    new Uri(neo4jSettings.Uri),
                    neo4jSettings.Username,
                    neo4jSettings.Password
                );

                client.ConnectAsync().Wait();
                return client;
            });

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
