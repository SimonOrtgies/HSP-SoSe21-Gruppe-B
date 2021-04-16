using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        
        


        static void Main(string[] args)
        {

            Schraube s = frageNachMassen(); //Objekt Schraube führt UNterprogramm aus


            s.printSchraube();  //Ausgabe der Daten die in der Schraube gespeichert sind (noch nicht fertig)


        }

        static public Schraube frageNachMassen()
        {

            Schraube schraube = new Schraube(); //Neues Objekt des Typ Schraube wird erstellt 

            Console.WriteLine("Willkommen beim Schraubenprogramm");
            //Variable um nach gültige EIngabe zu urteilen

            schraube.gewinderichtung = UserAbfrage.getGewinderichtung();

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);

            //schraube.gewindeLaenge = UserAbfrage.getGewindelenge();

            schraube.festigkeitsklasse = UserAbfrage.getFestigkeit();

            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }
    }
}
