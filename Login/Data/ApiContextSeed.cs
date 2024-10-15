using Login.Models.Entities;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;

namespace Login.Data;

public class ApiContextSeed
{
    public static async Task SeedRolesAsync(ApiContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Rols.Any())
            {
                var roles = new List<Rol>()
                        {
                            new Rol{Id=1, Nombre="Administrator"},
                            new Rol{Id=2, Nombre="Manager"},
                            new Rol{Id=3, Nombre="Employee"}
                        };
                context.Rols.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiContext>();
            logger.LogError(ex.Message);
        }
    }
}
