using Microsoft.AspNetCore.Mvc;

namespace MeetUp.EventsService.Api.Controllers
{
    public class EventsController : Controller
    {

        public EventsController() { }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync()
        {

            return Created(nameof(CreateEventAsync),123);
        }
    }
}
