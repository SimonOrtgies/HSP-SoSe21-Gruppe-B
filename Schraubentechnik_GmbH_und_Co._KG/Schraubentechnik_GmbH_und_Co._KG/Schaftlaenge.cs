﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public class Schaftlaenge  //Objekt Schaftlänge
    {
        public float schaftlaenge;
        public float minSchaftlaenge;
        public readonly float maxSchaftlaenge = 200; //Maximale Länge, würde ich einfach vorgeben   //Kann nicht überschrieben werden

        public Schaftlaenge(MetrischeGewindegroesse m)  //Schaftlänge mit der information des gewählten Gewindes wird konstruiert
        {

            minSchaftlaenge = MinSchaftlaengeRechnung(m.mutterhoehe);   //Berechnung der minimalen Schaftlänge (Unterprogramm ist weiter unten)
            schaftlaenge = minSchaftlaenge; //Schaftlänge wird auf minSchaftlänge gesetzt um mögliche KOnflikte vorzubeugen, falls schaftlänge keinen WErt hätte
        }


        public Boolean setSchaftlaenge(float s)     //Überprüfung ob die eingegebene Länge konform ist und auch setzung der Schaftlänge wenn korrekt
        {
            if (s >= minSchaftlaenge && s <= maxSchaftlaenge)    //Eingabe muss zwischen min und max Schaftlange liegen
            {
                schaftlaenge = s;   //Setzen der Schaftlänge
                //Console.WriteLine("Die gewählte Schaftlänge beträgt: " + s + " mm"); //Edit: Information über Eingabewert
                return true;    //Boolean gueltig in UserAbfrage.cs wird true
            }
            else
            {
                //Console.WriteLine("Die Länge der Schraube muss zwischen " + minSchaftlaenge + " mm und " + maxSchaftlaenge + " mm liegen"); //Hinweis in welchen Bereich die Schaftlänge nur liegen darf
                return false;   //Boolean gueltig in UserAbfrage.cs wird false
            }
        }

        public float MinSchaftlaengeRechnung(float mutterhoehe) //Berechnung der minimalen Schaftlänge
        {

            double d = Math.Round( 1 * mutterhoehe);
            float s = (float)d;
            return s;  //Rückgabe der minimalen Länge
        }

        public static float berechneSchaftlaenge(float mutterhoehe, float klemmlaenge)  //Unterprogramm um Schaftlänge zu berechnen
        {
            float s = mutterhoehe + klemmlaenge + 0.25f * mutterhoehe;    //Berchenung 
            
            float min = 3 * mutterhoehe;    //Erneute minSchaftlängen berechnung, weil so einfacher
            float max = 200;    //Erneute maxSchaftlängen festlegen
            if (s < min) //Wenn schaftlänge zu kurz
            {
                //Console.WriteLine("Zu kurze Klemmlänge für diese Größe, mindestens " + (min - 1.25 * mutterhoehe) + " mm nötig");
                s = -1;     //Gewollte fehler Rückmeldung

            }
            else if (s > max)    //WEnn Schaftlänge zu groß
            {
               // Console.WriteLine("Zu lange Klemmlänge für diese Größe, maximal " + (max - 1.25 * mutterhoehe) + " mm möglich");
                s = -1;//Gewollte fehler Rückmeldung
            }
            else
            {
                //Console.WriteLine("Die berechnete Schaftlänge beträgt: " + s + " mm");
            }
            return s;

        }


    }


}
