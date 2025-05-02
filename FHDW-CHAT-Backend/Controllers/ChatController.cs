using FHDW_CHAT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace ChatBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static readonly string FilePath = "chatlog.json";
        private static List<ChatMessage> messages = LoadChatLog();

        // GET api/chat
        [HttpGet]
        public IActionResult GetChatLog()
        {
            return Ok(messages);
        }

        // POST api/chat
        [HttpPost]
        public IActionResult SendMessage([FromBody] ChatMessage message)
        {
            // Save the incoming message
            message.TimeStamp = DateTime.UtcNow.ToString("o"); //2025-05-02T19:18:33.2947218Z Nur nötig wenn Client keinen TimeStamp sendet //TODO: Entscheiden ob Client oder Serverseitiges Handeln der TimeStamps!
            messages.Add(message);
            SaveChatLog();
            return NoContent();
        }

        private static List<ChatMessage> LoadChatLog() 
        {
            if(!System.IO.File.Exists(FilePath))
                    return new List<ChatMessage>();

            var json = System.IO.File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<ChatMessage>>(json) ?? new List<ChatMessage>();
        }

        private static void SaveChatLog()
        {
            var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
