﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public static class UserAbfrage
    {

        public static Gewinderichtung getGewinderichtung()
        {
            //AB HIER GEWINDERICHTUNG
            Boolean gueltig;    //Variable um nach gültige EIngabe zu urteilen
            Gewinderichtung r = Gewinderichtung.Rechtsgewinde; // Damit er etwas hat falls nichts zugewiesen wird / Gewinderichtung in .cs datei
            do
            {
                Console.WriteLine("Rechtsgewinde(r) oder Linksgewinde(l)");
                gueltig = true;
                string input = Console.ReadLine();

                if (input.Equals("l"))
                {
                    r = Gewinderichtung.Linksgewinde;   //Gewinderichtung.Linksgewinde ist funktion von Gewinderichtung.cs
                    Console.WriteLine("Es wurde ein Linksgewinde gewählt");
                }
                else if (input.Equals("r"))
                {
                    r = Gewinderichtung.Rechtsgewinde;
                    Console.WriteLine("Es wurde ein Rechtsgewinde gewählt");
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig); //Abfrage ob Wingabe gültig war

            return r; // gibt die Gewinderichtung zurück  
        }


        public static MetrischeGewindegroesse getMetrischegewindegroesse()
        {
            //AB HIER GEWINDEGRÖßE
            Boolean gueltig;
            MetrischeGewindegroesse g = null; // initialisierung, ein NULL-Wert wird zugewiesen, damit er was hat   /   siehe MetrischeGewindegroesse.cs
            do
            {
                gueltig = true;
                try     //Siehe kommentar bei catch
                {

                    Console.WriteLine("Geben Sie die gewünschte Gewindegröße ein");
                    float input = (float)Convert.ToDouble(Console.ReadLine());  //Float wird verwendet, weil weniger Speicherplatz als double / EInklammerung forciert den double einen FLoat zu werden
                    g = MassTabelle.getMetrischeGewindeG(input);    //siehe Masstabelle.cs
                    if (g == null)
                    {
                        Console.WriteLine("Nicht vorhanden");   //Wenn die eingegebene größe nicht gefunden wird, wird "null" returned und folgende ausgabe getätigt
                        gueltig = false;
                    }
                    else
                    {
                        string s = g.printGewinde();    //Ausgabe der technischen Daten des gewählten Gewindes
                        Console.WriteLine(s);
                    }



                }
                catch (Exception) //(sinngemäß: testen und verarbeiten) rahmt einen Block von Anweisungen (try statements) ein und legt Reaktionen (catch statementes) fest, die im Fehlerfall ausgeführt werden.
                {   //Wenn anstatt einer Zahl ein Buchstabe eingegeben wird, würde das Programm abstürzen. Durch try und catch wird der Fall abgefangen und folgendes ausgeführt:
                    Console.WriteLine("Bitte geben Sie nur eine Zahl ein (kein M)");
                    gueltig = false;
                }

            } while (!gueltig);
            return g;   //gibt die Gewindegröße g zurück
        }

        public static Schaftlaenge getSchaftlaenge(Schraube schraube)
        {
            //AB HIER SCHAFTLÄNGE
            Boolean gueltig;
            Boolean gueltigInnen = true;        //Für die inneren Schleife                    
            Schaftlaenge s = new Schaftlaenge(schraube.metrischeGewindegroesse);   //Neues Objekt Schaftlänge wird erstellt, mit der Gewindegröße als eingangsparameter um die minSchaftlänge aus der mutterhöhe berechnen zu können.

            do
            {
                gueltig = true;
                Console.WriteLine("Ist die Schaftlänge bekannt? (j/n)");    //Abfrage ob Schaftlänge bekannt
                string input = Console.ReadLine();
                if (input.Equals("n"))  //Wenn nein
                {
                    do
                    {
                        try     //Erneut try und catch zum fehler abfangen
                        {
                            gueltigInnen = true;
                            Console.WriteLine("Wie lang ist die Klemmlaenge (in mm)?");
                            float input2 = (float)Convert.ToDouble(Console.ReadLine());
                            s.schaftlaenge = Schaftlaenge.berechneSchaftlaenge(schraube.metrischeGewindegroesse.mutterhoehe, input2);   //Unterprogramm Schaftlänge in Schaftlaenge.cs aufrufen
                            if (s.schaftlaenge == -1)    //WEnn schaftlänge zu groß/klein kommt dieser Fehlercode wieder
                            {
                                gueltigInnen = false;
                            }
                        }
                        catch (Exception) //Fehler werden abgefangen
                        {
                            Console.WriteLine("Bitte eine Zahl eingeben");
                            gueltigInnen = false;
                        }
                    } while (!gueltigInnen);
                    return s;
                }
                else if (input.Equals("j")) //Wenn Schaftläenge bekannt soll diese einfach eingegeben werden
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("Welche Schaftlänge wird benötigt? (in mm)");
                            float input2 = (float)Convert.ToDouble(Console.ReadLine());
                            gueltigInnen = s.setSchaftlaenge(input2); //setSchaftlaenge überprüft ob die Eingabe im gültigen Bereich liegt. Berechnungen finden in Schaftlaenge.cs statt


                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Bitte eine Zahl eingeben");
                            gueltigInnen = false;
                        }
                    } while (!gueltigInnen);
                }
                else    //Falls falsche Eingabe
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig);
            return s;
        }

        public static Gewindelaenge getGewindelaenge(Schraube schraube)
        {
            //AB HIER GEWINDELÄNGE
            Boolean gueltig;
            //Für die innere Schleife 1. Ordnung                
            Gewindelaenge g = new Gewindelaenge(schraube.schaftLaenge, schraube.metrischeGewindegroesse);   //Neues Objekt Gewindelänge wird erstellt, mit der Schaftlänge und der Gewindegröße als Eingangsparameter um die minGewindelänge aus der mutterhöhe und der Schaftlänge berechnen zu können.

            do
            {
                gueltig = true;
                Console.WriteLine("Soll das Gewinde über die volle Schaftlänge gehen? (j/n)"); //Abfrage ob schaftLaenge=gewindeLaenge
                string input = Console.ReadLine();
                if (input.Equals("n"))  //Wenn nein
                {
                    g.gewindeLaenge = Gewindelaenge.benutzerdefinierteGewindelaenge(schraube);  //Unterprogramm in Gewindelaenge.cs

                }
                else if (input.Equals("j")) //Gewindelänge wird gleich der Schaftlänge gesetzt
                {
                    g.gewindeLaenge = Gewindelaenge.maxGewindeLaengeRechnung(schraube.schaftLaenge.schaftlaenge);  //maximal mögliche Gewindelänge (Schaftlänge) aus dem Unterprogramm in Gewindelaenge.cs mithilfe der Schaftlänge
                    Console.WriteLine("Die Gewindelänge beträgt: " + g.gewindeLaenge + " mm");
                    return g;
                }
                else    //Falls falsche Eingabe
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig);
            return g;
        }

        public static string getFestigkeit()
        {
            //AB HIER Festigkeitsklasse
            Boolean gueltig;    //Variable um nach gültige Eingabe zu urteilen
            string f = "5.8"; // Damit er etwas hat falls nichts zugewiesen wird
            do
            {
                Console.WriteLine("Wählen Sie ihre Festigkeitsklasse:5.8(1), 6.8(2), 8.8(3), 9.8(4), 10.9(5), 12.9(6)");
                gueltig = true;
                string input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    f = "5.8";
                }
                else if (input.Equals("2"))
                {
                    f = "6.8";
                }
                else if (input.Equals("3"))
                {
                    f = "8.8";
                }
                else if (input.Equals("4"))
                {
                    f = "9.8";
                }
                else if (input.Equals("5"))
                {
                    f = "10.8";

                }
                else if (input.Equals("6"))
                {
                    f = "12.9";
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig); //Abfrage ob Eingabe gültig war
            return f; // gibt die Festigkeitsklasse zurück zurück
        }
        public static string getSchraubenkopf()
        {
            //AB HIER Schraubenkopfwahl
            Boolean gueltig;    //Variable um nach gültige Eingabe zu urteilen
            string k = "Sechskant"; // Damit er etwas hat falls nichts zugewiesen wird
            do
            {
                Console.WriteLine("Wählen Sie ihren Schraubenkopf:Sechskant(1), Zylinderkopf mit Innensechskant(2),Senkkopf mit Innensechskant (3), Linsenkopf mit Schlitz(4)");
                gueltig = true;
                string input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    k = "Sechskant";
                }
                else if (input.Equals("2"))
                {
                    k = "Zylinderkopf mit Innensechskant";
                }
                else if (input.Equals("3"))
                {
                    k = "Senkkopf mit Innensechskant";
                }
                else if (input.Equals("4"))
                {
                    k = "Linsenkopf mit Schlitz";
                }

                else
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig); //Abfrage ob Eingabe gültig war
            return k;
        } // gibt den Schraubenkopf  zurück


        public static int getAnzahl()
        {
            Boolean gueltig;    //Gültigkeits Variable
            int anzahl = 1; //Variable für die ANzahl, anfangs auf 1 gesetzt. damit man was hat
            int max = 1000000; //Maximal abnehmbare ANzahl
            int min = 1;    //Minimal abnehmbare ANzahl

            do
            {
                try
                {
                    Console.WriteLine("Wie viele Exemplare dieser Schraube werden benötigt?");
                    gueltig = true;
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (input > max)    //Test ob zu viele Schrauben gewollt werden
                    {
                        Console.WriteLine("Es sind maximal " + max + " Schrauben möglich!");
                        gueltig = false;
                    }
                    else if (input < min)   //Test ob zu wenig Schrauben gewollt werden
                    {
                        Console.WriteLine("Es sind minimal " + min + " Schrauben nötig!");
                        gueltig = false;
                    }
                    else
                    {
                        anzahl = input; // Setzen der Anzahl, wenn die vorherigen Tests negativ waren
                    }
                }
                catch (Exception) //(sinngemäß: testen und verarbeiten) rahmt einen Block von Anweisungen (try statements) ein und legt Reaktionen (catch statementes) fest, die im Fehlerfall ausgeführt werden.
                {   //Wenn anstatt einer Zahl ein Buchstabe eingegeben wird, würde das Programm abstürzen. Durch try und catch wird der Fall abgefangen und folgendes ausgeführt:
                    Console.WriteLine("Ungültige Eingabe");
                    gueltig = false;
                }


            } while (!gueltig);
            return anzahl;  //Rückgabe der Anzahl
        }

    }
}
