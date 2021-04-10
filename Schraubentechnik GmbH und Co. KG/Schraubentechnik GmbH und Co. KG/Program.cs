using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        
        


        static void Main(string[] args)
        {

            Schraube s = frageNachMassen();
            Schraube s2 = frageNachMassen();
            s.printSchraube();
            s2.printSchraube();


            

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

            Schraube schraube = new Schraube();

            Console.WriteLine("Willkommen beim Schraubenprogramm");
            Boolean gueltig;
            do
            {
                Console.WriteLine("Rechtsgewinde(r) oder Linksgewinde(l)");
                gueltig = true;
                string input = Console.ReadLine();
                Gewinderichtung r = Gewinderichtung.Rechtsgewinde; // Damit er etwas hat falls nichts zugewiesen wird
                if (input.Equals("l"))
                {
                    r = Gewinderichtung.Linksgewinde;
                }
                else if (input.Equals("r"))
                {
                    r = Gewinderichtung.Rechtsgewinde;
                }
                else
                {
                    Console.WriteLine("ungültige eingabe!");
                    gueltig = false;
                }

                schraube.gewinderichtung = r; // speichert die Gewinderichtung in dem neuen Objekt schraube 
            } while (!gueltig);


            
            do
            {
                gueltig = true;
                try
                {
                    MetrischeGewindegroesse g = null; // initialisierung 
                    Console.WriteLine("Gib die Gewindegrösse ein mein Kerl!");
                    float input = (float)Convert.ToDouble(Console.ReadLine());
                    g = MassTabelle.getMetrischeGewindeG(input);
                    if (g == null)
                    {
                        Console.WriteLine("Nicht vorhanden");
                        gueltig = false;
                    }
                    else
                    {
                        schraube.metrischeGewindegroesse = g;
                        string s = g.printGewinde();
                        Console.WriteLine(s);
                    }



                }
                catch (Exception e)
                {
                    Console.WriteLine("Bist du behindert gib einfach Zahl ein??");
                    gueltig = false;
                }

            } while (!gueltig);

            
            return schraube;
        }
    }
}
