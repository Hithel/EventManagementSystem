using Login.Models.Entities;

namespace Login.Repositories.Interfaces;

public interface IUser : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByRefreshTokenAsync(string username);
}
