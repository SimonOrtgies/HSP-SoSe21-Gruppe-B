using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean Rechtsgewinde = true;
            string i;
            int j = 1;
            
            
            Console.WriteLine("Willkommen beim Schraubenprogramm");

            // Abfrage Rechtsgewinde - Linksgewinde

            while (j == 1 )
            {
                Console.WriteLine("Rechtsgewinde(r) oder Linksgewinde(l)");
                i = Console.ReadLine();
                if(i == "r")
                {
                    Rechtsgewinde = true;
                    j = 0;
                }
                else if(i == "l")
                {
                    Rechtsgewinde = false;
                    j = 0;
                }
                else 
                {
                    Console.WriteLine("Ungültige Eingabe!");
                }
            }
                
                

            //KONTROLLE
            if(Rechtsgewinde == true)
            {
                Console.WriteLine("Rechtsgewinde");
            }
            else
            {
                Console.WriteLine("Linksgewinde");
            }

            //Eingabe Gewindegröße

            Console.WriteLine("Geben Sie die Gewindegröße ein");


        }
    }
}
