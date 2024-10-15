using AutoMapper;
using Login.Repositories.Interfaces;
using Login.Services.Interfaces;

namespace Login.Services.Implementations;

public class GenericService<Vm, Dto, T> : IGenericService<Vm, Dto>
        where Vm : class
        where Dto : class
        where T : class
{
    protected readonly IGenericRepository<T> _repository;
    private readonly IMapper _mapper;

    protected GenericService(IGenericRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<Vm?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<Vm>(entity);
    }

    public virtual async Task<IEnumerable<Vm>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Vm>>(entities);
    }


    public virtual async Task<Vm> AddAsync(Dto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "El objeto DTO no puede ser nulo.");
        }

        var entity = _mapper.Map<T>(dto);
        await _repository.AddAsync(entity);

        return _mapper.Map<Vm>(entity);
    }

    public virtual async Task<Vm> UpdateAsync(Dto dto, int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException("Entity not found");
        }
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);

        return _mapper.Map<Vm>(entity);
    }
}
