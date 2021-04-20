using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        
        static void Main(string[] args)
        {
            StartAusgabe();

            int anzahl;
            anzahl = Schraubenanzahl();
      
            Schraube[] s = new Schraube[anzahl];    //Schraubenarray

            for (int i = 0; i < anzahl; i++)
            {
                Console.WriteLine("Parameter Für Schraube " + (i + 1) + " eingeben:");
                s[i] = frageNachMassen();
                s[i].volumen = getVolumen(s[i]);
                s[i].masse = getMasse(s[i]);
                s[i].preis = getPreis(s[i]);
                Console.WriteLine("");
                //Objekt Schraube führt UNterprogramm aus
            }

            for (int i = 0; i < anzahl; i++)
            {
                s[i].printSchraube(i);
                Console.WriteLine("");
                //Objekt Schraube führt UNterprogramm aus
            }


            
        }

        static void StartAusgabe()
        {
            Console.WriteLine("Willkommen beim Schraubenprogramm von Schraubentechnik GmbH und Co. KG.");
            Console.WriteLine("");
            Console.WriteLine("Hier können Sie Ihre individuellen Schrauben mit folgenden Parametern erstellen:");
            Console.WriteLine("-Anzahl individueller Schrauben");
            Console.WriteLine("-Gewinderichtung");
            Console.WriteLine("-Gewindegröße");
            Console.WriteLine("-Schaftlänge");
            Console.WriteLine("-Gewindelänge");
            Console.WriteLine("-Festigkeit");
            Console.WriteLine("-Anzahl");
            Console.WriteLine("");
        }

        static int Schraubenanzahl()
        {
            int anzahl = 0;
            Boolean gueltig;
            Console.WriteLine("Wie viele individuell verschiedene Schrauben sollen erstellt werden?");
            do
            {
                gueltig = true;
                try
                {
                    anzahl = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig);
            Console.WriteLine("");

            return anzahl;
        }

        static public Schraube frageNachMassen()
        {

            Schraube schraube = new Schraube(); //Neues Objekt des Typ Schraube wird erstellt 

            schraube.gewinderichtung = UserAbfrage.getGewinderichtung();

            schraube.schraubenkopf = UserAbfrage.getSchraubenkopf();

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);

            schraube.gewindeLaenge = UserAbfrage.getGewindelaenge(schraube);

            schraube.festigkeitsklasse = UserAbfrage.getFestigkeit();

            schraube.anzahl = UserAbfrage.getAnzahl();

            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }

        static double getVolumen(Schraube schraube)
        {
            
            double pi = 3.14159;
            double faktorschraubenkopf = 1.5;
            double schraubenkopf = schraube.metrischeGewindegroesse.mutterhoehe * schraube.metrischeGewindegroesse.bezeichnung * faktorschraubenkopf * schraube.metrischeGewindegroesse.bezeichnung * faktorschraubenkopf * pi /4; //Volumen Schraubenkopf
            double V = schraubenkopf + schraube.anzahl * ( schraube.gewindeLaenge.gewindeLaenge * schraube.metrischeGewindegroesse.flanken * schraube.metrischeGewindegroesse.flanken * pi  / 4 + (schraube.schaftLaenge.schaftlaenge - schraube.gewindeLaenge.gewindeLaenge) * schraube.metrischeGewindegroesse.bezeichnung * schraube.metrischeGewindegroesse.bezeichnung * pi /4); // Volumenberechnung
            return V;
        }

        static double getMasse(Schraube schraube)
        {
            double dichte = 0.00000785;
            double m = dichte * schraube.volumen;
            return m;
        }

        static double getPreis(Schraube schraube)
        {
            double p = schraube.masse * 5; // Preisberechnung
            return p;
        }
    }
}
