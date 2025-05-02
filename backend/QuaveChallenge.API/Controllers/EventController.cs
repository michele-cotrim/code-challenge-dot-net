using Microsoft.AspNetCore.Mvc;

namespace QuaveChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        [HttpGet("communities")]
        public IActionResult GetCommunities()
        {
            // TODO: Implement get communities
            return Ok();
        }

        [HttpGet("people/{communityId}")]
        public IActionResult GetPeople(int communityId)
        {
            // TODO: Implement get people by community
            return Ok();
        }

        [HttpPost("check-in/{personId}")]
        public IActionResult CheckIn(int personId)
        {
            // TODO: Implement check-in
            return Ok();
        }

        [HttpPost("check-out/{personId}")]
        public IActionResult CheckOut(int personId)
        {
            // TODO: Implement check-out
            return Ok();
        }

        [HttpGet("summary/{communityId}")]
        public IActionResult GetSummary(int communityId)
        {
            // TODO: Implement get summary
            return Ok();
        }
    }
} 