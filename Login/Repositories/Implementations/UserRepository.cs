using Login.Data;
using Login.Models.Entities;
using Login.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Login.Repositories.Implementations;

public class UserRepository : GenericRepository<User>, IUser
{
    private readonly ApiContext _context;

    public UserRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async virtual Task<bool> IsExists(string userName)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            var exists = await _context.Users.AnyAsync(p => p.Username == userName);
            return exists;
        }

        return false;
    }


    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username != null && u.Username.ToLower() == username.ToLower());
    }
    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public override async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
