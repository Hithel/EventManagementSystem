using EventCreation.Models.Entities;

namespace EventCreation.Repositories.interfaces;
public interface IEventRepository
    {
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> CreateEvent(Event evt);
        Task<Event> UpdateEvent(Event evt, int id);
        Task<Event> DeleteEvent(int id);
    }

