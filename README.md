# Remote App

Remote App ist derzeit der ASP.NET-Core-Host einer geplanten Anwendung zur Fernsteuerung eines Hosts. Der aktuelle Stand stellt eine Razor-Seite zur Prozessabfrage und eine kleine HTTP-API bereit. Ein separater Client und weitere Fernsteuerungsfunktionen sind noch nicht implementiert.

## Voraussetzungen

- .NET 10 SDK

## Starten

Im Repository-Verzeichnis:

```powershell
dotnet restore RemoteApp.slnx
dotnet run --project RemoteApp/RemoteApp.csproj --launch-profile http
```

Die HTTP-Entwicklungsprofile starten die Anwendung unter `http://localhost:5000`. Die Startprofile sind in `RemoteApp/Properties/launchSettings.json` definiert.

Zum Erstellen der Lösung:

```powershell
dotnet build RemoteApp.slnx
```

## Aktuelle Funktionen

- `/` zeigt eine einfache Oberfläche zur Abfrage eines Prozessnamens.
- `GET /System/Process/{name}` liefert Prozessname, Laufstatus und Anzahl gefundener Instanzen. Bei einem ungültigen Namen antwortet die API mit `400 Bad Request`.
- `GET /Test/Text` und `GET /Test/Html` sind Beispiel-Endpunkte.

Die HTTP-Beispiele für VS Code oder Visual Studio liegen in `RemoteApp/RemoteApp.http`.

## Sicherheit

Der Prozessstatus-Endpunkt besitzt derzeit keine Authentifizierung. Wer die Anwendung erreichen kann, kann Statusinformationen zu erratbaren Prozessnamen abfragen. Die lokalen Entwicklungsprofile binden an `localhost`; vor einem Netzwerk- oder Produktionseinsatz müssen Zugriffsschutz und HTTPS für die konkrete Hosting-Umgebung eingerichtet werden.
