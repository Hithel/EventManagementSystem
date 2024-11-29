using EventCreation.Models.Dtos;
using EventCreation.Models.Vms;
using EventCreation.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventCreation.Controllers
{
    public class EventController : ApiBaseController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("CreateEventAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EventVm>> CreateEventAsync([FromBody] EventDto dto)
        {
            var result = await _eventService.CreateEventAsync(dto);
            return Ok(result);
        }

        [HttpDelete("DeleteEventAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EventVm>> DeleteEventAsync(int id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            return Ok(result);
        }

        [HttpGet("GetAllEventsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<EventVm>>> GetAllEventsAsync()
        {
            var result = await _eventService.GetAllEventsAsync();
            return Ok(result);
        }

        [HttpGet("GetEventByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EventVm>> GetEventByIdAsync(int id)
        {
            var result = await _eventService.GetEventByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("UpdateEventAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EventVm>> UpdateEventAsync([FromBody] EventDto dto, int id)
        {
            var result = await _eventService.UpdateEventAsync(dto, id);
            return Ok(result);
        }
    }
}
