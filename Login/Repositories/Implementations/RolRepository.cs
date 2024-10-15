using Login.Data;
using Login.Models.Entities;
using Login.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Login.Repositories.Implementations;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly ApiContext _context;

    public RolRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Rols
            .ToListAsync();
    }

    public override async Task<Rol?> GetByIdAsync(int id)
    {
        return await _context.Rols
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Rol?> GetRolByName(string name)
    {
        var rol = await _context.Rols
                                .Where(r => r.Nombre.ToLower() == name.ToLower())
                                .FirstOrDefaultAsync();

        return rol != null ? rol : null;

    }


}

