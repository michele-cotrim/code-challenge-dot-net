using Microsoft.AspNetCore.Mvc;
using QuaveChallenge.API.Contracts;
using QuaveChallenge.API.Services;

namespace QuaveChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        public readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("communities")]
        [ProducesResponseType(typeof(CommunityResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommunities()
        {
            var response = await _eventService.GetCommunitiesAsync();
            return Ok(response);
        }

        [HttpGet("people/{communityId}")]
        [ProducesResponseType(typeof(PersonResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeople(int communityId)
        {
            var response = await _eventService.GetPeopleByEventAsync(communityId);
            return Ok(response);
        }

        [HttpPost("check-in/{personId}/{communityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckIn(int personId, int communityId)

        {
            await _eventService.CheckInPersonAsync(personId, communityId);

            return Ok();
            
        }

        [HttpPost("check-out/{personId}/{communityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult CheckOut(int personId, int communityId)
        {
            _eventService.CheckOutPersonAsync(personId, communityId).Wait();
            return Ok();
        }

        [HttpGet("allow/check-out/{personId}/{communityId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]

        public async Task<IActionResult> AbleToCheckOut(int personId, int communityId)
        {
            var response = await _eventService.AllowCheckOutPersonAsync(personId, communityId);
            return Ok(response);
        }

        [HttpGet("allow/check-in/{personId}/{communityId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]

        public async Task<IActionResult> AbleToCheckin(int personId, int communityId)
        {
            var response = await _eventService.AllowCheckInPersonAsync(personId, communityId);
            return Ok(response);
        }

        [HttpGet("summary/{communityId}")]
        [ProducesResponseType(typeof(EventSummaryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummary(int communityId)
        {
            var response = await _eventService.GetEventSummaryAsync(communityId);
            return Ok(response);
        }
    }
} 