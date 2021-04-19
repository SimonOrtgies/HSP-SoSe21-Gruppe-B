using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        
        


        static void Main(string[] args)
        {
            int anzahl = 1;
            Console.WriteLine("Wie viele verschiedene Schrauben sollen erstellt werden?");
            anzahl = Convert.ToInt32(Console.ReadLine());
            
            Schraube[] s = new Schraube[anzahl];

            // Schraube s = frageNachMassen(); //Objekt Schraube führt UNterprogramm aus
            for (int i = 0; i < anzahl; i++)
            {
                s[i] = frageNachMassen();
                //Objekt Schraube führt UNterprogramm aus
            }

            //Schraube s[] = frageNachMassen(); //Objekt Schraube führt UNterprogramm aus
            for (int i = 0; i < anzahl; i++)
            {
                s[i].printSchraube(i);
                //Objekt Schraube führt UNterprogramm aus
            }

        }

        static public Schraube frageNachMassen()
        {

            Schraube schraube = new Schraube(); //Neues Objekt des Typ Schraube wird erstellt 

            Console.WriteLine("Willkommen beim Schraubenprogramm");
            //Variable um nach gültige EIngabe zu urteilen

            schraube.gewinderichtung = UserAbfrage.getGewinderichtung();

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);

            schraube.gewindeLaenge = UserAbfrage.getGewindelaenge(schraube);

            schraube.festigkeitsklasse = UserAbfrage.getFestigkeit();

            schraube.anzahl = UserAbfrage.getAnzahl();

            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }
    }
}
