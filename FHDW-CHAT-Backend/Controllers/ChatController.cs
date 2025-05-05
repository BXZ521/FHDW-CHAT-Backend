using FHDW_CHAT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ChatBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static readonly string FilePath = "chatlog.json";

        // GET Chat/getChatLog
        [HttpGet]
        [Route("getChatLog")]
        public IActionResult GetChatLog()
        {
            return Ok(LoadChatLog());
        }

        // POST Chat/sendMessage
        [HttpPost]
        [Route("sendMessage")]
        public IActionResult SendMessage([Required, FromBody] ChatMessage message)
        {
            // Save the incoming message
            message.TimeStamp = DateTime.UtcNow.ToString("o"); //2025-05-02T19:18:33.2947218Z Nur nötig wenn Client keinen TimeStamp sendet //TODO: Entscheiden ob Client oder Serverseitiges Handeln der TimeStamps!
            List<ChatMessage> messages = LoadChatLog();
            messages.Add(message);
            SaveChatLog(messages);
            return NoContent();
        }

        private static List<ChatMessage> LoadChatLog() 
        {
            if(!System.IO.File.Exists(FilePath))
                    return new List<ChatMessage>();

            var json = System.IO.File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<ChatMessage>>(json) ?? new List<ChatMessage>();
        }

        private static void SaveChatLog(List<ChatMessage> messages)
        {
            var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
