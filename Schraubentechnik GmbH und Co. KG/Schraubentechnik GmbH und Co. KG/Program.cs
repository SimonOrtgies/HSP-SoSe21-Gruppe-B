using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        
        


        static void Main(string[] args)
        {

            Schraube s = frageNachMassen(); //Objekt Schraube führt UNterprogramm aus
            //Schraube s2 = frageNachMassen();  //Um weitere Schraube zu erstellen in einem Programm


            s.printSchraube();  //Ausgabe der Daten die in der Schraube gespeichert sind (noch nicht fertig)
            //s2.printSchraube();


            

            //Console.WriteLine("M ");

            



            //Boolean Rechtsgewinde = true;
            //string i;
            //int j = 1;
            
            
            //Console.WriteLine("Willkommen beim Schraubenprogramm");

            //// Abfrage Rechtsgewinde - Linksgewinde

            //Boolean eingabeGueltig = false;

            //do
            //{
            //    Console.WriteLine("Rechtsgewinde(r) oder Linksgewinde(l)");
            //    i = Console.ReadLine();
            //    if (i == "r")
            //    {
            //        Rechtsgewinde = true;
            //        eingabeGueltig = true;
            //    }
            //    else if (i == "l")
            //    {
            //        Rechtsgewinde = false;
            //        eingabeGueltig = true;
            //    }
            //    else
            //    {
            //        Console.WriteLine("ungültige eingabe!");
            //    }
            //} while (!eingabeGueltig);
                
                

            ////KONTROLLE
            //if(Rechtsgewinde == true)
            //{
            //    Console.WriteLine("Rechtsgewinde");
            //}
            //else
            //{
            //    Console.WriteLine("Linksgewinde");
            //}

            ////Eingabe Gewindegröße

            //Console.WriteLine("Geben Sie die Gewindegröße ein");


        }

        static public Schraube frageNachMassen()
        {

            Schraube schraube = new Schraube(); //Neues Objekt des Typ Schraube wird erstellt 

            Console.WriteLine("Willkommen beim Schraubenprogramm");
            //Variable um nach gültige EIngabe zu urteilen

            schraube.gewinderichtung = UserAbfrage.getGewinderichtung();

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);



            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }
    }
}
