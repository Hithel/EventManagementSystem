using EventCreation.Services.implementation;
using EventCreation.Services.interfaces;

namespace EventCreation.Extensions.ServiceRegistration;
    public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration["ConnectionStrings:DefaultConnection"]
                                  ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        services.AddTransient<ConnectionFactory.ConnectionFactory>(_ => new ConnectionFactory.ConnectionFactory(connectionString!));

        services.AddScoped<IEventService, EventService>();

        return services;
    }
}

