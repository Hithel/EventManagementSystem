

using Microsoft.AspNetCore.Identity;
using Login.Helpers.Authentication.Security;
using Login.Helpers.Services;
using Login.Helpers.TwoStepAuth;
using Login.Models.Entities;
using Login.Services.Implementations;
using Login.Services.Interfaces;


namespace Login.Extensions.ServiceRegistration;

public static class ServiceRegistrationExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        //Servicio de entidades
        service.AddScoped<IRolService, RolService>();
        service.AddScoped<IUserService, UserService>();


        //Servicios de Authorization
        service.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


        //servicios de Two Step Authentication
        service.AddScoped<IAuth, Auth>();

        //Servicio de hasheo de contrasena
        service.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        //Servicio de generacion de token
        service.AddScoped<ITokenService, TokenService>();

    }
}
