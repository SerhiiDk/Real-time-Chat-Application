using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ChatApplication.API.Infrastructure.CORS;

public static class DefaultCorsSetting
{
    public static string PolicyName { get; private set; } = "CorsPolicy";
    public static void RegisterDefaultCORS(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(PolicyName,
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .WithOrigins("https://realtimechat-a6exejdched6e4fb.northeurope-01.azurewebsites.net", "https://localhost:7253")
                       .AllowCredentials();
            }));
    }
}
