

using Login.Repositories.Implementations;
using Login.Repositories.Interfaces;

namespace Login.Extensions.RepositoryRegistration
{
    public static class RepositoryRegistrationExtensions
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IRol, RolRepository>();
            service.AddScoped<IUser, UserRepository>();
        }
    }
}
