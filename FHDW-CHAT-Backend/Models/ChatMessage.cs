namespace FHDW_CHAT_Backend.Models
{
    public class ChatMessage
    {
        public string Author { get; set; } //Author der Nachricht, u.a. genutzt für Nachrichten-Zuweisung (eigene vs. Fremdnachricht)
        public string Message { get; set; } //Inhalt der Nachricht
        public string TimeStamp { get; set; } //Zeitstempel für chronologische Einordnung
        public string Addressee { get; set; } //Adressat, normalerweise "/@alle" bei spezifischer Addressierung "/@'USER'"
    }
    /*
    Im JSON-Log werden hierduch diese Objekte erstellt:
    
    {
    "Author": "Tim",
    "Message": "Hallo Benjamin",
    "TimeStamp": "2025-05-30T14:45:31.4378781Z",
    "Addressee": "/@alle"
    }
    */
}
