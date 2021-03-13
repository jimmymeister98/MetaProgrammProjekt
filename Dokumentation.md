![.Net version ](https://img.shields.io/badge/.NET-5.0-blue)
# MetaProgrammProjekt Dokumentation

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

## Komplikationen und deren Lösungen (Designentscheidungen)
- 1..N und 1..* Beziehungen

  In diesem Fall wird eine Listenklasse erstellt(anstatt Plural vom Klassennamen -> KlassennameListe)

- Freistehende Objekte

  In diesem Fall wird falls ein Objekt der Json noch nicht als `*.cs` Datei vorhanden ist angelegt und eine Grundinitialisierung wird vorgenommen.

- Kompositionen

  Kompositionen werden in den Konstruktoren realisiert. Dateien die eine Komposition mit einer anderen Datei eingehen werden erfasst. So können "Unberührte" bzw Kernklassen
einen Default Konstruktor erhalten.

## Benutzung
- Auswählen der .Json datei, sollte diese nicht von umple kommen so erscheint eine fehlermeldung
- Auswählen des Zielordner in der die Klassenbibliothek erstellt werden soll (Name der Klassenbibliothek = Name der Json)
- Auf Konvertieren Clicken.
