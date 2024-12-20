namespace ChatApplication.API.Infrastructure.AzureSignalRSettings;

public static class AzureSignalRSetting
{
    public static IServiceCollection RegisterAzureSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        var signalRConnectionString = configuration.GetSection("SignalR").GetValue<string>("Connection");

        services.AddSignalR().AddAzureSignalR(options =>
        {
            options.ConnectionString = signalRConnectionString;
            options.ApplicationName = "ChatUI";
        });

        return services;
    }
}
