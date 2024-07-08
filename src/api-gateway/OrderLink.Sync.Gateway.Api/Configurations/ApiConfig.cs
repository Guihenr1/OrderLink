using Microsoft.Net.Http.Headers;

namespace OrderLink.Sync.Gateway.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services,
                                                      IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                            .WithOrigins("https://localhost:6001",
                                            "http://localhost:6002",
                                            "http://192.168.250.107:7000",
                                            "https://assyncdev.alter-solutions.com/*")
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithHeaders(HeaderNames.ContentType, "Access-Control-Allow-Origin"));

                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithOrigins("https://localhost:6005",
                                         "http://localhost:6006",
                                         "http://192.168.250.108:7000",
                                         "https://assyncprod.alter-solutions.com/*")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithHeaders(HeaderNames.ContentType, "Access-Control-Allow-Origin"));
            });

            return services;
        }
    }
}
