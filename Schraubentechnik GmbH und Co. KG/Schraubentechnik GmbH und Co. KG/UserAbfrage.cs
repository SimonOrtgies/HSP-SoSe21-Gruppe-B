using System;
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
                }
                else if (input.Equals("r"))
                {
                    r = Gewinderichtung.Rechtsgewinde;
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
                catch (Exception e) //(sinngemäß: testen und verarbeiten) rahmt einen Block von Anweisungen (try statements) ein und legt Reaktionen (catch statementes) fest, die im Fehlerfall ausgeführt werden.
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
                            if(s.schaftlaenge == -1)    //WEnn schaftlänge zu groß/klein kommt dieser Fehlercode wieder
                            {
                                gueltigInnen = false;
                            }                        
                        }
                        catch (Exception e) //Fehler werden abgefangen
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
                        catch (Exception e)
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

        //public static float getGewindelaenge()
        //{

        // }


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

    }
}
