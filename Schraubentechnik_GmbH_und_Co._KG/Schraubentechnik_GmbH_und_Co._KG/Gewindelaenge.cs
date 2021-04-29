using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public class Gewindelaenge
    {
        public float gewindeLaenge;
        public float minGewindeLaenge;
        public float maxGewindeLaenge;

        public Gewindelaenge(Schaftlaenge s, MetrischeGewindegroesse m) //neues Objekt Gewindelänge
        {
            minGewindeLaenge = minGewindeLaengeRechnung(m.mutterhoehe);
            maxGewindeLaenge = maxGewindeLaengeRechnung(s.schaftlaenge);
            gewindeLaenge = maxGewindeLaenge;
        }


        public static float minGewindeLaengeRechnung(float mutterhoehe)
        {
            float g = 2 * mutterhoehe; //minimale Gewindelänge, kann noch verändert werden
            return g;
        }
        public static float maxGewindeLaengeRechnung(float schaftlaenge)
        {
            float g = schaftlaenge; //Gewindelänge = Schaftlänge
            return g;
        }

        public static float berechneGewindeLaenge(float mutterhoehe, float schaftlaenge, float g)  //Unterprogramm um Gewindelänge zu berechnen
        {
            float min = minGewindeLaengeRechnung(mutterhoehe);    //Erneute minGewindelängenBerechnung, weil so einfacher
            float max = maxGewindeLaengeRechnung(schaftlaenge);    //Erneute maxGewindelängen festlegen
            if (g < min) //Wenn Gewindelänge zu kurz
            {
                Console.WriteLine("Zu kurze Gewindelänge für diese Größe, mindestens " + min + " mm nötig");
                g = -1;     //gewollte Fehlerrückmeldung

            }
            else if (g > max)    //Wenn Gewindelänge zu groß
            {
                Console.WriteLine("Zu lange Gewindelänge für diese Größe, maximal " + max + " mm möglich");
                g = -1;     //gewollte Fehlerrückmeldung
            }
            else
            {
                Console.WriteLine("Die gewählte Gewindelänge beträgt: " + g + " mm");
            }
            return g;
        }

        public static float benutzerdefinierteGewindelaenge(Schraube schraube)  //Unterprogramm für eingegebene Schrauben, abrufbar von UserAbfrage.cs
        {
            Boolean gueltig;
            Gewindelaenge g = new Gewindelaenge(schraube.schaftLaenge, schraube.metrischeGewindegroesse);
            do
            {
                try
                {
                    gueltig = true;
                    Console.WriteLine("Ist die Gewindelänge bekannt(1) oder soll die minimal nötige Gewindelänge genutzt werden(2)?"); //Abfrage ob Gewindelänge bekannt
                    int input2 = Convert.ToInt32(Console.ReadLine());  //Benutzereingabe

                    if (input2 == 1)
                    {
                        g.gewindeLaenge = bekannteGewindelaenge(schraube); //Unterprogramm weiter unten

                    }
                    else if (input2 == 2)
                    {
                        g.gewindeLaenge = minGewindeLaengeRechnung(schraube.metrischeGewindegroesse.mutterhoehe); //minimal nötige Gewindelänge aus dem Unterprogramm in Gewindelaenge.cs mithilfe der mutterhoehe
                        Console.WriteLine("Die Gewindelänge beträgt " + g.gewindeLaenge + " mm");
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe");
                        gueltig = false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Ungültige Eingabe");
                    gueltig = false;
                }

            } while (!gueltig);

            return g.gewindeLaenge;
        }
        public static float bekannteGewindelaenge(Schraube schraube)
        {
            Boolean gueltig;
            Gewindelaenge g = new Gewindelaenge(schraube.schaftLaenge, schraube.metrischeGewindegroesse);
            do
            {
                try     //try und catch zum fehler abfangen
                {
                    gueltig = true;
                    Console.WriteLine("Wie lang ist die Gewindelänge (in mm)?");
                    float input3 = (float)Convert.ToDouble(Console.ReadLine());
                    g.gewindeLaenge = berechneGewindeLaenge(schraube.metrischeGewindegroesse.mutterhoehe, schraube.schaftLaenge.schaftlaenge, input3);   //Unterprogramm Schaftlänge in Schaftlaenge.cs aufrufen
                    if (g.gewindeLaenge == -1)    //Automatischer Fehlercode wenn gewählte Gewindelänge zu groß/klein (siehe Schaftlänge)
                    {
                        gueltig = false;
                    }
                }
                catch (Exception) //Fehler werden abgefangen
                {
                    Console.WriteLine("Bitte eine Zahl eingeben");
                    gueltig = false;
                }

            } while (!gueltig);

            return g.gewindeLaenge;
        }
    }
}
