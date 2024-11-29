using AutoMapper;
using EventCreation.Models.Dtos;
using EventCreation.Models.Entities;
using EventCreation.Models.Vms;

namespace EventCreation.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EventDto, Event>().ReverseMap();
            CreateMap<Event, EventVm>().ReverseMap();
        }
    }
}
