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
                Console.WriteLine("");
                //Objekt Schraube führt UNterprogramm aus
            }

            for (int i = 0; i < anzahl; i++)
            {
                s[i].printSchraube(i);
                Console.WriteLine("");
                //Objekt Schraube führt UNterprogramm aus
            }

            Volumen(s);
            Preis();
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
                catch (Exception e)
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

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);

            schraube.gewindeLaenge = UserAbfrage.getGewindelaenge(schraube);

            schraube.festigkeitsklasse = UserAbfrage.getFestigkeit();

            schraube.anzahl = UserAbfrage.getAnzahl();

            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }

        static float Volumen(Schraube schraube)
        {
            float Volumen = schraube.anzahl * (schraube.gewindeLaenge * metrischeGewindegroesse.flanken + (schraube.schaftLaenge.schaftlaenge - schraube.gewindeLaenge) * schraube.metrischeGewindegroesse );
        }

        static float Preis()
        {

        }
    }
}
