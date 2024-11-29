using EventCreation.Models.Dtos;
using EventCreation.Models.Vms;

namespace EventCreation.Services.interfaces
{
    public interface IEventService
    {
        Task<EventVm> GetEventByIdAsync(int id);
        Task<IEnumerable<EventVm>> GetAllEventsAsync();
        Task<EventVm> CreateEventAsync(EventDto dto);
        Task<EventVm> UpdateEventAsync(EventDto dto, int id);
        Task<EventVm> DeleteEventAsync(int id);
    }
}
