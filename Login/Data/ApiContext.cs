using Login.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Login.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {

    }

    public DbSet<Rol> Rols { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRol> UsersRol { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /* dotnet ef migrations add InitialCreate --project .\Login --startup-project .\Login\ --output-dir ./Data/Migrations
       */
    /* dotnet ef database update --project .\Login\ --startup-project .\Login\
     */
}
