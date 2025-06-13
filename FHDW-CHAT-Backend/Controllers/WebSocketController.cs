using FHDW_CHAT_Backend.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace FHDW_CHAT_Backend.Controllers
{
    public class WebSocketController
    {
        //Liste der verbundenen Websocket-Clients
        private static readonly List<WebSocket> ConnectedClients = new();

        //Pfad zur Chatlog-Datei
        private static readonly string FilePath = "chatlog.json";

        //Lock-Objekt zur Synchronisierung des Zugriffs auf shared Resources
        private static readonly object LockObj = new();


        /// Behandelt eingehende WebSocket-Verbindungen.
        public static async Task Handle(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                lock (LockObj) ConnectedClients.Add(webSocket);

                await SendChatLog(webSocket);
                await ReceiveLoop(webSocket);

                lock (LockObj) ConnectedClients.Remove(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        // Sendet den aktuellen Chatlog an einem sich neu verbindenten Client
        private static async Task SendChatLog(WebSocket socket)
        {
            var chatLog = LoadChatLog();
            var json = JsonSerializer.Serialize(chatLog);
            await socket.SendAsync(Encoding.UTF8.GetBytes(json), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        
        // Verarbeitung und Broadcasting eingehender Nachrichten
        private static async Task ReceiveLoop(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                    break;
                }

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var message = JsonSerializer.Deserialize<ChatMessage>(json);
                if (message != null)
                {
                    //Zeitstempel setzen (lokal(Server) oder Alternativ, siehe Kommentar, UTC-Zeit)
                    message.TimeStamp = DateTime.Now.ToString("o"); // Für generalisierte Speicherung auf UTC0: message.TimeStamp = DateTime.UtcNow.ToString("o");

                    //Chatverlauf laden, Nachricht anhängen und speichern
                    var messages = LoadChatLog();
                    messages.Add(message);
                    SaveChatLog(messages);

                    //Broadcast an alle Clients
                    var outJson = JsonSerializer.Serialize(messages);
                    await Broadcast(outJson);
                }
            }
        }

        // Sendet eine Nachricht an alle verbundenen Clients
        private static async Task Broadcast(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var tasks = new List<Task>();

            lock (LockObj)
            {
                foreach (var client in ConnectedClients.Where(c => c.State == WebSocketState.Open))
                {
                    tasks.Add(client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None));
                }
            }

            await Task.WhenAll(tasks);
        }

        // Konvertierung JSON-Log --> Liste
        private static List<ChatMessage> LoadChatLog()
        {
            if (!File.Exists(FilePath)) return new List<ChatMessage>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<ChatMessage>>(json) ?? new List<ChatMessage>();
        }

        // Speicherung der Liste in der JSON-Log-File
        private static void SaveChatLog(List<ChatMessage> messages)
        {
            var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
