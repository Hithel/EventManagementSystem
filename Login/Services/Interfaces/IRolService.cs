using Login.Models.Dtos.Rol;
using Login.Models.ViewModels.Rol;

namespace Login.Services.Interfaces;

public interface IRolService : IGenericService<RolVm, RolDto>
{
    Task<RolVm> GetRolNameByNameAsync(string name);
}
