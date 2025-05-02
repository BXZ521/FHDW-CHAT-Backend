using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChatBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        // In-memory storage for messages
        private static List<string> _messages = new List<string>();

        // GET api/chat
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_messages);
        }

        // POST api/chat
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            // Save the incoming message
            _messages.Add(message);
            return Ok();
        }
    }
}
