using Login.Models.Entities;

namespace Login.Repositories.Interfaces;

public interface IRol : IGenericRepository<Rol>
{
    Task<Rol?> GetRolByName(string name);
}
