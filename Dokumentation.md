![.Net version ](https://img.shields.io/badge/.NET-5.0-blue)
# MetaProgrammProjekt Dokumentation
Author: Jimmy Neitzert
Mat.Nr: Der E-Mail zu entnehmen.
Stand: 15.03.2021

## Ursprüngliches Problem
Das ursprüngliche Problem, bzw. die Aufgabe war es aus einem UML Diagramm (erstellt mit [UmpleOnline](https://cruise.umple.org/umpleonline/) eine Klassenbibliothek zu erstellen.
Diese sollte die Abhängigkeiten zwischen den Objekten und deren Attribute darstellen.

## Allgemeine Anforderungen
- Einlesen eines UML diagrammes, welches im Json format abgespeichert wurde
- Umwandeln dieser in Klassen mit Attributen & Listen
- Erstellen einer Klassenbibliothek
- Dokumentation

## Lösungsansatz 
- Maske zur Eingabe über Windows Forms
- Ordnerauswahl und Dateiauswahl über `FolderBrowserDialog fbd = new FolderBrowserDialog();` und `OpenFileDialog choofdlog = new OpenFileDialog();` um die Benutzung intuitiv zu halten
- Erstellen der Klassenbibliothek über CLI
- Einlesen der Json mit [Json.Net](https://www.newtonsoft.com/json) da dieses Framework einfach, free for commercial use und hocheffizient ist.
- Erstellen der Dateien über `File.WriteAllLines(path,string[]`
- Schreiben in die Dateien über `File.AppendAllLines(path,string)` und `File.WriteAllLines(path,string[]`

## Komplikationen und deren Lösungen & Designentscheidungen
- 1..N und 1..* Beziehungen

  In diesem Fall wird eine Listenklasse erstellt(anstatt Plural vom Klassennamen -> KlassennameListe)

- Freistehende Objekte

  In diesem Fall wird falls ein Objekt der Json noch nicht als `*.cs` Datei vorhanden ist angelegt und eine Grundinitialisierung wird vorgenommen.

- Kompositionen

  Kompositionen werden in den Konstruktoren realisiert. Dateien die eine Komposition mit einer anderen Datei eingehen werden erfasst. So können "Unberührte" bzw Kernklassen
einen Default Konstruktor erhalten.

- Wieso so viele for-loops? (JsonReader.cs)

  Die for-loops dienen als "Module", jeder for-loop erfüllt seine Aufgabe wie z.B: Anlegen der Datei, Hinzufügen von Abhängigkeiten usw. Durch diese Modularität ist es einfach 
das Programm zu Debuggen oder zu erweitern, da man immer genau weiss was, woher kommt und wohin es soll.


## Was ist was?

- JsonReader.cs
  - Diese Klasse prüft ob die json von Umple erstellt wurde, wenn ja dann:
  - Lese Json ein `File.ReadAllText(PathJson)`
  - Basisinitialisierung (using direktiven, setzen des Namespaces,Erstellung der Individuellen Daten)
  - falls 1..* relation zwischen 2 Objekten dann erstelle Listenklasse.
  - Relationen Hinzufügen durch auslesen der `"umpleAssociations"`
  - Freistehende Klassen ohne Relationen Initialisieren
  - Attribute mithilfe `"[umpleClasses][attributes]"` hinzufügen.
  - Kompositionen und dementsprechende Konstruktoren mithilfe `"[MultiplicityOne]"` hinzufügen
  - Basiskonstruktoren zu Kernklassen hinzufügen.
  - Ausstehende Brackets schließen.

- KonsolenHandler.cs
  - Festlegen des Pfades für den Start der Cmd.exe
  - Von diesem Pfad wird `"dotnet new classlib --force -o " + filename` in die Konsole weitergeleitet um das Erstellen eines Projektes zu beginnen.
  - Warten auf abschluss dieses Prozesses, da ohne diesen der JsonReader keine Dateien erstellen kann, weil der Ordner fehlt.

- Form1.cs
  - User-Interface
  - Handling des Explorers
  - Prüfen der Pfade
  - Aufrufen der `JsonReader.cs` und `KonsolenHandler.cs`

## Benutzung
- Herunterladen des [Programmes](https://github.com/jimmymeister98/MetaProgrammProjekt/releases) (mit einer Test Json)
- Auswählen der .Json datei, sollte diese nicht von umple kommen so erscheint eine fehlermeldung
- Auswählen des Zielordner in der die Klassenbibliothek erstellt werden soll (Name der Klassenbibliothek = Name der Json)
- Auf Konvertieren Clicken.
