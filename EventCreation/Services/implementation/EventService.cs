using AutoMapper;
using EventCreation.Models.Dtos;
using EventCreation.Models.Entities;
using EventCreation.Models.Vms;
using EventCreation.Repositories.interfaces;
using EventCreation.Services.interfaces;

namespace EventCreation.Services.implementation
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EventVm> CreateEventAsync(EventDto dto)
        {
            var entity = _mapper.Map<Event>(dto);
            var result = await _repository.CreateEvent(entity);
            return _mapper.Map<EventVm>(result);
        }

        public async Task<EventVm> DeleteEventAsync(int id)
        {
            var result = await _repository.DeleteEvent(id);
            return _mapper.Map<EventVm>(result);
        }

        public async Task<IEnumerable<EventVm>> GetAllEventsAsync()
        {
            var result = await _repository.GetAllEvents();
            return _mapper.Map<IEnumerable<EventVm>>(result);
        }

        public async Task<EventVm> GetEventByIdAsync(int id)
        {
            var result = await _repository.GetEventById(id);
            return _mapper.Map<EventVm>(result);
        }

        public async Task<EventVm> UpdateEventAsync(EventDto dto, int id)
        {
            var eventDto = _mapper.Map<Event>(dto);
            var result = await _repository.UpdateEvent(eventDto, id);
            return _mapper.Map<EventVm>(result);
        }
    }
}
