# Projekt Webentwicklung: Chat-System mit Server-Client-Architektur

## Gruppenmitglieder
- Benjamin A. Schmitz  
- Tim L. Stöcker

---

## 💡 Idee
Unsere Projektidee ist die Entwicklung eines Chat-Systems, bei dem mehrere Clients über einen zentralen Server miteinander kommunizieren können. Dabei entsteht eine einfache, textbasierte Web-Applikation, die es ermöglicht, in Echtzeit Nachrichten zwischen verschiedenen Teilnehmern auszutauschen.

---

## 🚀 Mehrwert
- Echtzeit-Kommunikation zwischen mehreren Nutzern
- Verständnis und Anwendung moderner Webarchitekturen mit einer Web-API
- Privater Chat
- Skalierbar
- Eigenständige und individuelle Weiterentwickelung
- Hohe und kostengünstige Anpassbarkeit (Customization)

---

## ✅ Anforderungen

### Funktionale Anforderungen
- Aufbau einer Verbindung zwischen Client und Server
- Senden und Empfangen von Nachrichten in Echtzeit
- Automatische Aktualisierung des Chats in allen verbundenen Browsern ohne manuelles Neuladen
- Darstellung der Nachrichten in chronologischer Reihenfolge
- Darstellung des Benutzernamens

### Nicht-funktionale Anforderungen
- Stabiler Serverbetrieb
- Gute Performance bei mehreren gleichzeitigen Nutzern
- Intuitive und benutzerfreundliche Oberfläche
- Erweiterbarkeit für zukünftige Features
- Kostengünstiger Betrieb des Systems

---

## 🌟 Mögliche Erweiterungen (Nice to have)
- Anzeige von Profilbild, Nickname und ggf. Online-Status
- Zeitstempel bei Nachrichten
- Dark Mode / Theme-Switcher
- Chatverlauf speichern (z.B. JSON, SQLite, MySQL)
- Upload und Versand verschiedener Dateiformate (z.B. PDFs, Bilder)
- Direkte Anzeige hochgeladener oder verlinkter Bilder im Chatverlauf
- Integration von kleinen Spielen, Features oder einem Chatbot (z.B. mittels ChatGPT)
- Emoji-Unterstützung via Picker
- Nachrichten bearbeiten/löschen
- Push Notifications bei neuen Nachrichten
- Mobile-optimiertes Design (Responsive Webdesign)

---

## 🎯 Zusammenhang zur Aufgabenstellung
Die Idee entspricht der Anforderung, eine Web-Applikation mit Client-Server-Kommunikation zu entwickeln. Der Fokus liegt auf der Interaktion zwischen mehreren Clients über einen zentralen Server – ein typisches Beispiel für moderne, verteilte Web-Systeme.

---

## 🧰 Eingesetzte Technologien

- **Frontend (Client)**: React (JavaScript oder TypeScript)
- **API-Kommunikation**: OpenAPI zur Spezifikation der REST-Schnittstellen
- **Backend (Server)**: C# (.NET Core) oder PHP
- **Datenübertragung**:
  - REST-API für Basisfunktionen
  - WebSockets für Echtzeitübertragung von Nachrichten
- **Dateihandling**: ggf. `cURL` für Datei-Uploads
- **Versionsverwaltung & Doku**: Git & GitHub (inkl. Markdown-Dokumentation)
