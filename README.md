# Terminkalender
Das Projekt hat die typischen Funktionalitäten eines einfachen Terminkalenders.

## Aufgabe
Das Projekt wurde im Rahmen der Vorlesung "Software Engineering I" als Testat vorgegeben. Der Terminkalender soll die Möglichkeit bieten Termine zu erfassen, darzustellen und löschen zu können. Zusätzlich sollen die Termine in einer Datei persistent gespeichert und beim Programmstart wieder geladen werden. In den folgenden Absätzen werden die Funktionalitäten näher erläutert.

## Übersicht
Mit Hilfe des Terminkalenders sollen verschiedene Arten von Terminen verwaltet werden können. Als Basisfunktionalität muss mindestens folgendes implementiert werden:

* Eingabe eines Termins
* Ausgabe der Termine des aktuellen Tages
* Ausgabe der Termine der nächsten sieben Tage
* Löschen eines Termins
* Löschen aller Termine eines Tages

Die gesamte Benutzerführung kann dabei mit Hilfe von Konsolen Ein-/Ausgabe durchgeführt werden.

## Termine
Neben einmaligen Terminen, also Terminen die an genau einem Zeitpunkt stattfinden, sollen auch wiederkehrende Termine verwaltet werden können. Bei Terminen wird zwischen ganztägigen Terminen und Terminen die eine Dauer haben unterschieden.

* Ganztägige Termine haben dabei eine Dauer von mindestens einem und höchstens 365 Tagen.
* Termine mit Dauer haben einen Start- und einen Endzeitpunkt. Diese beiden Zeitpunkte müssen nicht zwingend am selben Tag sein.

Sowohl ganztägige Termine als auch Termine mit einer Dauer können als einmalige Termine und als Termine mit einer Wiederholung vorliegen. Als Wiederholungsintervall sind dabei mindestens vorzusehen:

* jede Woche
* jedes Jahr

## Eingabe
Bei der Eingabe eines Termins soll der Anwender durch die notwendigen Schritte geführt werden. Eine minimale Überprüfung der Benutzereingaben ist dabei ausreichend.

## Darstellung
Bei der Ausgabe der Termine soll die Möglichkeit angeboten werden, dass die Termine des aktuellen Tages und die Termine der kommenden sieben Tage ausgegeben werden. Sind für die Zeiträume keine Termine eingeplant, soll eine passende Meldung ausgegeben werden. Auch bei der Ausgabe ist auf einen minimalen Komfort zu achten.

## Löschen
Zukünftige Termine sollen auch gelöscht werden können. Bei einem einmaligen Termin ist nur eine nachgeschaltete Sicherheitsabfrage notwendig. Handelt es sich bei dem zu löschenden Termin um einen Termin mit Wiederholung, so muss abgefragt werden ob nur dieser eine Termin oder dieser Termin und alle zukünftigen Wiederholungen gelöscht werden sollen.
