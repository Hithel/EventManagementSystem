namespace Login.Extensions.CorsConfiguration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
           });
}
