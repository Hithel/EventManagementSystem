using EventCreation.Repositories.implementation;
using EventCreation.Repositories.interfaces;

namespace EventCreation.Extensions.RepositoryRegistration
{
    public static class RepositoryRegistrationExtensions
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
