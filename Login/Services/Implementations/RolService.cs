using AutoMapper;
using Login.Models.Dtos.Rol;
using Login.Models.Entities;
using Login.Models.ViewModels.Rol;
using Login.Repositories.Interfaces;
using Login.Services.Interfaces;

namespace Login.Services.Implementations;

public class RolService : GenericService<RolVm, RolDto, Rol>, IRolService
{
    private readonly IRol _rolRepository;
    private readonly IMapper _mapper;

    public RolService(IRol rolRepository, IMapper mapper)
        : base(rolRepository, mapper)
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
    }

    public async Task<RolVm> GetRolNameByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("El nombre del rol no puede ser nulo o vacío.", nameof(name));
        }

        var rol = await _rolRepository.GetRolByName(name);

        if (rol == null)
        {
            throw new KeyNotFoundException($"Rol no encontrado para el nombre: {name}");
        }
        return _mapper.Map<RolVm>(rol);

    }

}

