using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Schraubentechnik_GmbH_und_Co._KG
{

    public class Schraube  //Objekt SChraube
    {
        public Gewinderichtung gewinderichtung; //Variablen/Informationen die die Schraube beeinhaltet
        public MetrischeGewindegroesse metrischeGewindegroesse;
        public Schaftlaenge schaftLaenge;
        public float gewindeLaenge;
        public string festigkeitsklasse;
        public int anzahl;

        public Schraube (Gewinderichtung g, Schaftlaenge schaftL, float gewindeL)  //Aufbau der Struktur
        {
            gewinderichtung = g;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }

        public Schraube(Schaftlaenge schaftL, float gewindeL)
        {
            gewinderichtung = Gewinderichtung.Rechtsgewinde;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }
        public Schraube()   //Leerer Konstruktor
        {
          
        }

        public void printSchraube() //Unterprogramm zur Ausgabe der Schraubeninformationen
        {
            Console.WriteLine("");
            Console.WriteLine("INFOS über die Schraube:");
            Console.WriteLine("Gewinderichtung: " + gewinderichtung);
            Console.WriteLine("Gewindegröße: M" + metrischeGewindegroesse.bezeichnung);
            Console.WriteLine("Schaftlänge: " + schaftLaenge.schaftlaenge + " mm");
            Console.WriteLine("Festigkeitsklasse: " + festigkeitsklasse);
            Console.WriteLine("Anzahl: " + anzahl);
        }
    }
}
