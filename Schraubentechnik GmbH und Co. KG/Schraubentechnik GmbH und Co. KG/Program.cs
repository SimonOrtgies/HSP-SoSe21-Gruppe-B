using System;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        static void Main()
        {
            int AnzahlProgramme = 0;

            while(AnzahlProgramme < 3)
            {
                Console.WriteLine("(1)Gewindeart auswählen");
                Console.WriteLine("(2)Thema2");
                Console.WriteLine("(3)Thema3");

                string Programmnummer = Console.ReadLine();

                Console.Clear();

                switch (Programmnummer)
                {
                    case "1":
                        Gewindeart();
                        AnzahlProgramme++;
                        break;
                    case "2":
                        Thema2();
                        AnzahlProgramme++;
                        break;
                    case "3":
                        Thema3();
                        AnzahlProgramme++;
                        break;
                    default:
                        Console.WriteLine("wählen Sie ein mögliches Programm aus");
                        break;
                };

            }
            

        }
        static void Gewindeart()
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

        static void Thema2()
        {
            Console.WriteLine("Thema2");
        }

        static void Thema3()
        {
            Console.WriteLine("Thema3");
        }
    }
}
