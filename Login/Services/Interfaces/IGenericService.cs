namespace Login.Services.Interfaces;

public interface IGenericService<Vm, Dto> where Vm : class where Dto : class
{
    Task<Vm?> GetByIdAsync(int id);
    Task<IEnumerable<Vm>> GetAllAsync();
    Task<Vm> AddAsync(Dto dto);
    Task<Vm> UpdateAsync(Dto dto, int id);
}
