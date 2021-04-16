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
       
        public Gewindelaenge(Schaftlaenge s,MetrischeGewindegroesse m) //neues Objekt Gewindelänge
        {
            minGewindeLaenge = minGewindeLaengeRechnung(m.mutterhoehe);
            maxGewindeLaenge = maxGewindeLaengeRechnung(s.schaftlaenge);
            gewindeLaenge = maxGewindeLaenge;
        }

        
        public static float minGewindeLaengeRechnung(float mutterhoehe)
        {
            float g = 2 * mutterhoehe; //wirrwarr Rechnung
            return g;
        }
        public static float maxGewindeLaengeRechnung(float schaftlaenge)
        {
            float g = schaftlaenge; //Gewindelänge = Schaftlänge
            return g;
        }

        public static float berechneGewindeLaenge(float mutterhoehe, float schaftlaenge, float Eingabewert)  //Unterprogramm um Gewindelänge zu berechnen
        {
            float g = Eingabewert;
            float min = 2 * mutterhoehe;    //Erneute minGewindelängenBerechnung, weil so einfacher
            float max = schaftlaenge;    //Erneute maxGewindelängen festlegen
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
        //MÖGLICHES SCHREIBEN EINES UNTERPROGRAMMES STATT VERKETTETER SCHLEIFEN
        //public static float bekannteGewindelaenge(Schraube schraube)
        //{
        //   Boolean gueltigInnen;
        //    Gewindelaenge g = new Gewindelaenge(schraube.schaftLaenge, schraube.metrischeGewindegroesse);
        //    do
        //    {
        //        try     //try und catch zum fehler abfangen
        //        {
        //            gueltigInnen = true;
        //            Console.WriteLine("Wie lang ist die Gewindelänge (in mm)?");
        //            float input3 = (float)Convert.ToDouble(Console.ReadLine());
        //            g.gewindeLaenge = berechneGewindeLaenge(schraube.metrischeGewindegroesse.mutterhoehe, schraube.schaftLaenge.schaftlaenge, input3);   //Unterprogramm Schaftlänge in Schaftlaenge.cs aufrufen
        //            if (g.gewindeLaenge == -1)    //Automatischer Fehlercode wenn gewählte Gewindelänge zu groß/klein (siehe Schaftlänge)
        //            {
        //                gueltigInnen = false;
        //            }
        //        }
        //        catch (Exception) //Fehler werden abgefangen
        //        {
        //            Console.WriteLine("Bitte eine Zahl eingeben");
        //            gueltigInnen = false;
        //        }
        //
        //    } while (!gueltigInnen);
        //
        //    return g;
        //}
    }
}
